﻿namespace LegacyApp;

public interface IClientRepository
{
    Client GetClientById(int clientId);
}