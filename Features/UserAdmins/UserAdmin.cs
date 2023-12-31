﻿using Flunt.Notifications;

namespace PoolbetIntegration.API.Features.UserAdmins;

public sealed class UserAdmin : Notifiable<Notification>
{
    public UserAdmin() { }

    private UserAdmin(string username, string name, string email, decimal credit)
    {
        Username = username;
        Name = name;
        Email = email;
        Credit = credit;
    }

    public int UserAdminId { get; set; }
    public string Username { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public decimal Credit { get; set; }

    public static UserAdmin Create(string username,
                            string name,
                            string email,
                            decimal credit)
    {
        var userAdmin = new UserAdmin(username: username,
                                      name: name,
                                      email: email,
                                      credit: credit);

        return userAdmin;
    }

    public void VerifyValue(decimal value, int type)
    {
        if (value > Credit)
        {
            AddNotification("UserAdmin.Credit", "Amount is greater than current balance.");
            return;
        }
        else if (value < Credit && type == 1)
        {
            Decrease(value);
            return;
        }
        else if (value < Credit && type == 2 || type == 4)
        {
            Increase(value);
            return;
        }
        
    }


    public void Increase(decimal value)
    {
        Credit += value;
    }

    public void Decrease(decimal value)
    {
        Credit -= value;
    }

    public UserAdminDTO ToDTO()
    {
        return new UserAdminDTO
        {
            UserAdminId = UserAdminId,
            Name = Name,
            Email = Email,
            Username = Username,
            Credit = Credit,
        };
    }
}
