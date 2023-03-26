using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace asp_MVC_letsTry.Models
{
    public class loginForm
    {
        [BindNever]
        public int id { get; set; }

        [Display(Name ="Введіть своє ім'я")]
        [StringLength(30)]
        [Required(ErrorMessage ="Це поле не може бути пустим")]
        public string name { get; set; }

        [Display(Name = "Введіть своє прізвіще")]
        [StringLength(30)]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string surname { get; set; }

        [Display(Name = "Введіть пароль")]
        [StringLength(24, MinimumLength = 8, ErrorMessage = "Пароль повинен бути від 8 до 24 символів")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль занадто короткий")]
        public string password { get; set; }

        [Display(Name = "Введіть пошту")]
        [StringLength(35)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Невірна пошта")]
        public string email { get; set; }
    }
}
