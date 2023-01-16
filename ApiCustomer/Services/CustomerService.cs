using System.Text.RegularExpressions;

namespace ApiCustomer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new();
        public long AddCustomer(Customer customer)
        {
            customer.Cpf = new Regex("[.-]").Replace(customer.Cpf, string.Empty);
            EmailAlreadyExists(customer.Email);
            CpfAlreadyExists(customer.Cpf);
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
            EmailAlreadyExists(customer.Email, customer.Id);
            CpfAlreadyExists(customer.Cpf, customer.Id);
            customer.Id = Id;
            var index = _customers.FindIndex(customer => customer.Id == Id);
            _customers[index] = customer;
        }

        private void EmailAlreadyExists(string email, long id = 0)
        {

            if (_customers.Any(customer => customer.Email == email && customer.Id != id)) { throw new ArgumentException("Email already exists!"); }
        }

        private void CpfAlreadyExists(string cpf, long id = 0)
        {
            if (_customers.Any(customer => customer.Cpf == cpf && customer.Id != id)) { throw new ArgumentException("Cpf already exists!"); }
        }

        private void IdExists(long id)
        {
            if (_customers.Any(customer => customer.Id == id)) return;
            throw new Exception($"{id} not found!");
        }
    }
}
