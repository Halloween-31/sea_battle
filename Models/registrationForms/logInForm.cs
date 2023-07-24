using System.ComponentModel.DataAnnotations;

namespace asp_MVC_letsTry.Models.registrationForms
{
    public class logInForm
    {
        public int id { get; set; }

        [Display(Name = "Введіть пароль")]
        [StringLength(24, MinimumLength = 8, ErrorMessage = "Пароль повинен бути від 8 до 24 символів")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Неправильний пароль")]
        public string password { get; set; }

        [Display(Name = "Введіть пошту")]
        [StringLength(35)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Невірна пошта")]
        public string email { get; set; }

        public logInForm() { }
    }
}
