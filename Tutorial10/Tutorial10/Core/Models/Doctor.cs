﻿namespace Tutorial10.Core.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public virtual ICollection<Prescription> Prescriptions { get; set; }
}