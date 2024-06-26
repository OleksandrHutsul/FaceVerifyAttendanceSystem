﻿namespace FaceVerifyAttendanceSystem.BL.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public List<string>? Roles { get; set; }
    }
}