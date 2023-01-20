using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiCustomer.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly List<Customer> _customers = new();
        public long AddCustomer(Customer customer)
        {
            customer.Cpf.FormatCPF();
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
            if (_customers.Any(customer => customer.Id == Id)) 
                return _customers.FirstOrDefault(customer => customer.Id == Id);
            throw new Exception($"Customer for ID: {Id} not found!");
        }

        public void DeleteCustomer(long id)
        {
            var customer = GetCustomerById(id);
            _customers.Remove(customer);
        }

        public void UpdateCustomer(long Id, Customer customer)
        {
            if (_customers.Any(customer => customer.Id == Id))
            {
                customer.Id = Id;
                var index = _customers.FindIndex(customer => customer.Id == Id);
                _customers[index] = customer; 
            }
            throw new Exception($"Customer for ID: {Id} not found!");
        }

        private void CustomerAlreadyExists(string email, string cpf)
        {

            if (_customers.Any(customer => customer.Email == email)) 
                
                throw new ArgumentException($"Customer already exists for email: {email}");
            
            if (_customers.Any(customer => customer.Cpf == cpf))

                throw new ArgumentException($"Customer already exists for CPF: {cpf}");

        }
    }
}
