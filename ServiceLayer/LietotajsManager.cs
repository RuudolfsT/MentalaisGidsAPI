using DomainLayer.Auth;
using MentalaisGidsAPI.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceLayer.Interface;

namespace ServiceLayer
{
    public class LietotajsManager : BaseManager<Lietotajs>, ILietotajsManager
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
            var user = _context
                .Lietotajs.Include(l => l.LietotajsLoma)
                .ThenInclude(ll => ll.LomaNosaukumsNavigation)
                .SingleOrDefault(l => l.Lietotajvards == model.Lietotajvards);

            if (user == null)
            {
                return null;
            }

            var passwordHasher = new PasswordHasher<Lietotajs>();

            var verificationResult = passwordHasher.VerifyHashedPassword(
                user,
                user.Parole,
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

        public RegisterResponse? Register(RegisterRequest model)
        {
            if (_context.Lietotajs.Any(l => l.Lietotajvards == model.Lietotajvards))
            {
                return null;
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

            string hashedPassword = passwordHasher.HashPassword(newUser, model.Parole);

            newUser.Parole = hashedPassword;

            _context.Lietotajs.Add(newUser);
            _context.SaveChanges();

            return new RegisterResponse
            {
                LietotajsID = newUser.LietotajsID,
                Lietotajvards = newUser.Lietotajvards
            };
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
