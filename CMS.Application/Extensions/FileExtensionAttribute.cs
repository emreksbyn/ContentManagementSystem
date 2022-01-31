using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace CMS.Application.Extensions
{
    // Kullanıcının bizim istediğimiz formatta resim eklemesini sağlayacak kodları bu class' ta yazacağız. ValidationAttribute' dan kalıtım alması gerekmektedir. Kendi attribute sınıfımız ..
    public class FileExtensionAttribute : ValidationAttribute
    {
        // ValidationAttribute sınıfından IsValid fonks. override ettik.
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            IFormFile file = value as IFormFile;
            if (file != null)
            {
                var extension = Path.GetExtension(file.FileName);
                string[] extensions = { "jpg", "jpeg" };
                bool result = extensions.Any(x => extension.EndsWith(x));
                if (!result)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
            return ValidationResult.Success;
        }

        private string GetErrorMessage()
        {
            return "Yalnızca jpg ve jpeg uzantılarına izin verilir !";
        }
    }
}
