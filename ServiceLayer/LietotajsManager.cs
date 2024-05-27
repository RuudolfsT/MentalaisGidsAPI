using System.Text;
using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;
using Microsoft.AspNetCore.Identity;
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

        public void Register(RegisterRequest model)
        {
            if (_context.Lietotajs.Any(l => l.Lietotajvards == model.Lietotajvards))
            {
                //šāds lietotājs ar šādu lietotājvārdu jau ir, ko darīt?
            }

            var passwordHasher = new PasswordHasher<Lietotajs>();

            Lietotajs newUser =
                new()
                {
                    Lietotajvards = model.Lietotajvards,
                    Vards = model.Vards,
                    Uzvards = model.Uzvards,
                    Epasts = model.Epasts,
                    Dzimums = model.Dzimums
                };

            string hashedPassword = passwordHasher.HashPassword(newUser, model.Parole.ToString());

            newUser.Parole = Encoding.UTF8.GetBytes(hashedPassword);

            _context.Lietotajs.Add(newUser);
            _context.SaveChanges();
        }

        public IEnumerable<Lietotajs> GetAll()
        {
            return _context.Lietotajs;
        }

        public void Delete(Lietotajs model)
        {
            _context.Lietotajs.Remove(model);
            _context.SaveChanges();
        }
    }
}
