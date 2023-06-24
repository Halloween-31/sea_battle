using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace asp_MVC_letsTry.Models.registrationForms
{
    public class signUpForm
    {
        public int id { get; set; }

        [Display(Name = "Введіть своє ім'я")]
        [StringLength(30, ErrorMessage = "Не більше 30 символів")]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string name { get; set; }

        [Display(Name = "Введіть своє прізвіще")]
        [StringLength(30, ErrorMessage = "Не більше 30 символів")]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string surname { get; set; }

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

        public signUpForm() { }
    }
}
