using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    internal class LietotajsManager : BaseManager<Lietotajs>, ILietotajsManager
    {
        private readonly MentalaisGidsContext _context;
        private IJwt _jwt;

        public LietotajsManager(MentalaisGidsContext context, IJwt jwt)
            : base(context)
        {
            _jwt = jwt;
            _context = context;
        }

        public AuthenticateResponse? Authenticate(AuthenticateRequest model)
        {
            var user = _context.Lietotajs.SingleOrDefault(l =>
                l.Lietotajvards == model.Lietotajvards
            );

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<Lietotajs>();

            var verificationResult = passwordHasher.VerifyHashedPassword(
                user,
                user.Parole.ToString(),
                model.Parole
            );

            if (verificationResult != PasswordVerificationResult.Success)
            {
                return null;
            }

            return new AuthenticateResponse
            {
                LietotajsID = user.LietotajsID,
                Vards = user.Vards,
                Uzvards = user.Uzvards,
                Lietotajvards = user.Lietotajvards,
                Token = _jwt.GenerateToken(user)
            };
        }

        public IEnumerable<Lietotajs> GetAll()
        {
            throw new NotImplementedException();
        }

        public Lietotajs GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Register(RegisterRequest model)
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }
    }
}
