using HeroesDal;
using HeroesDal.Models;
using HeroesServices.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HeroesServices.Services
{
    public class TrainerService : ITrainerService
    {
        private HeroesDBContext _db;
        private EncryptionService _encryptionService;
        public TrainerService(HeroesDBContext db,
                              EncryptionService encryptionService)
        {
            _db = db;
            _encryptionService = encryptionService;
        }
        public bool Register(TrainerReq trainerReq)
        {
            ValidatePassword(trainerReq.Password);
            trainerReq.Password = _encryptionService.sha256Encrypt(trainerReq.Password);
            Trainer trainer = trainerReq.ToEntity();
            _db.Trainers.Add(trainer);
            _db.SaveChanges();
            return true;
        }

        private void ValidatePassword(string password, out string ErrorMessage)
        {
            var input = password;
            ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
                throw new Exception("Password should not be empty");

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{8,15}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
                throw new Exception("Password should contain At least one lower case letter");

            else if (!hasUpperChar.IsMatch(input))
                throw new Exception("Password should contain At least one upper case letter");

            else if (!hasMiniMaxChars.IsMatch(input))
                throw new Exception("Password should not be less than or greater than 12 characters");


            else if (!hasNumber.IsMatch(input))
                throw new Exception("Password should contain At least one numeric value");


            else if (!hasSymbols.IsMatch(input))
                throw new Exception("Password should contain At least one special case characters");
        }
    }
}
