using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Dto
{
    public class UserDto
    {
        public UserDto()
        {

        }
        public UserDto(int id, string cpf, string name, string email, string login, int role)
        {
            Id = id;
            CPF = cpf;
            Name = name;
            Email = email;
            Login = login;
            Role = role;
        }
        public int Id { get; set; }

        public string CPF { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }
        public string Login { get; set; }
        public int Role { get; set; }
    }
}
