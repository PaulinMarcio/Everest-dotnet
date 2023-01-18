using System.Text.RegularExpressions;

namespace ApiCustomer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new();
        public long AddCustomer(Customer customer)
        {
            customer.Cpf = new Regex("[.-]").Replace(customer.Cpf, string.Empty);
            CustomerAlreadyExists(customer.Email, customer.Cpf);
            customer.Id = _customers.LastOrDefault()?.Id + 1 ?? 1;
            _customers.Add(customer);
            return customer.Id;
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public Customer GetCustomerById(long Id)
        {
            IdExists(Id);
            return _customers.FirstOrDefault(customer => customer.Id == Id)!;
        }

        public void DeleteCustomer(long id)
        {
            var customer = GetCustomerById(id);
            _customers.Remove(customer);
        }

        public void UpdateCustomer(long Id, Customer customer)
        {
            IdExists(Id);
            customer.Id = Id;
            var index = _customers.FindIndex(customer => customer.Id == Id);
            _customers[index] = customer;
        }

        private void CustomerAlreadyExists(string email, string cpf)
        {

            if (_customers.Any(customer => customer.Email == email)) 
                
                throw new ArgumentException($"Customer already exists for email: {email}");
            
            if (_customers.Any(customer => customer.Cpf == cpf))

                throw new ArgumentException($"Customer already exists for CPF: {cpf}");

        }

        private void IdExists(long id)
        {
            if (_customers.Any(customer => customer.Id == id)) return;
                throw new Exception($"Customer for ID: {id} not found!");
        }
    }
}
