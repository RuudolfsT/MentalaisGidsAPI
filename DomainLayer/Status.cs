using System.Collections.Generic;
namespace DomainLayer
{
    public class Status
    {
        public bool Success { get; set; }
        public List<string> Errors { get; }

        public Status(bool success)
        {
            Success = success;
            Errors = new List<string>();
        }

        public void AddError(string message)
        {
            Success = false;
            Errors.Add(message);
        }

        public void ClearErrors()
        {
            Errors.Clear();
        }
    }
}