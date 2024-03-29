﻿using System.ComponentModel.DataAnnotations;

namespace asp_MVC_letsTry.Models.loginForms_try
{
    public class loginForm_tryDB
    {
        //static int idCounter = 0;

        //[BindNever]
        public int id { get; set; }

        [Display(Name = "Введіть своє ім'я")]
        [StringLength(30)]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string name { get; set; }

        [Display(Name = "Введіть своє прізвіще")]
        [StringLength(30)]
        [Required(ErrorMessage = "Це поле не може бути пустим")]
        public string surname { get; set; }

        [Display(Name = "Введіть пароль")]
        [StringLength(24, MinimumLength = 8, ErrorMessage = "Пароль повинен бути від 8 до 24 символів")]
        [DataType(DataType.Password)]
        //[Required(ErrorMessage = "Пароль занадто короткий")]
        public string password { get; set; }

        [Display(Name = "Введіть пошту")]
        [StringLength(35)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Невірна пошта")]
        public string email { get; set; }

        /*public loginForm(string name, string surname, string password, string email)
        {
            id = idCounter++;

            this.name = name;
            this.surname = surname;
            this.password = password;
            this.email = email;
        }*/
    }
}