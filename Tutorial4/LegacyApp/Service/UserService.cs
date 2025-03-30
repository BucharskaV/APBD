using System;

namespace LegacyApp
{
    public class UserService 
    {
        private readonly IUserValidator _userValidator;
        private readonly IClientRepository _clientRepository;
        private readonly IUserCreditService _userCreditService;

        public UserService() : this(new UserValidator(), new ClientRepository(), new UserCreditService()) { }
        
        public UserService(IUserValidator userValidator, IClientRepository clientRepository, IUserCreditService userCreditService)
        {
            _userValidator = userValidator;
            _clientRepository = clientRepository;
            _userCreditService = userCreditService;
        }

        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (_userValidator.Validate(firstName, lastName, email, dateOfBirth))
            {
                return false;
            }

            var client = _clientRepository.GetClientById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (client.Type == ClientType.VeryImportantClient)
            {
                user.HasCreditLimit = false;
            }
            else if (client.Type == ClientType.ImportantClient)
            {
                int creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                creditLimit = creditLimit * 2;
                user.CreditLimit = creditLimit;
            }
            else
            {
                user.HasCreditLimit = true;
                int creditLimit = _userCreditService.GetCreditLimit(user.LastName, user.DateOfBirth);
                user.CreditLimit = creditLimit;
            }

            if (user.HasCreditLimit && user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

    }
}
