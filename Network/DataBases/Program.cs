﻿using Microsoft.Extensions.DependencyInjection;

namespace DataBases;


public class Program
{
    static void Main(string[] args)
    {
        using (var dbContext = new ChatContext()) ;

    }
}
