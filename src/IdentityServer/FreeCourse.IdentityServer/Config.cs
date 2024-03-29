﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System;
using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace FreeCourse.IdentityServer
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_catalog") {Scopes = {"catalog_full_permission"}},
            new ApiResource("resource_photo_stock") {Scopes = {"photo_stock_full_permission"}},
            new ApiResource("resource_basket") {Scopes = {"basket_full_permission"}},
            new ApiResource("resource_discount") {Scopes = {"discount_full_permission"}},
            new ApiResource("resource_order") {Scopes = {"order_full_permission"}},
            new ApiResource("resource_payment") {Scopes = {"payment_full_permission"}},
            new ApiResource("resource_gateway") {Scopes = {"gateway_full_permission"}},
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
        };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    Name = "roles",
                    DisplayName = "Roles",
                    Description = "Users roles",
                    UserClaims = new[] {"role"}
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("catalog_full_permission", "Catalog API icin full erisim"),
                new ApiScope("photo_stock_full_permission", "Photo Stock API icin full erisim"),
                new ApiScope("basket_full_permission", "Basket API icin full erisim"),
                new ApiScope("discount_full_permission", "Discount API icin full erisim"),
                new ApiScope("order_full_permission", "Order API icin full erisim"),
                new ApiScope("payment_full_permission", "Payment API icin full erisim"),
                new ApiScope("gateway_full_permission", "Gateway API icin full erisim"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client()
                {
                    ClientName = "Asp.Net Core Mvc",
                    ClientId = "WebMvcClient",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes =
                    {
                        "catalog_full_permission",
                        "photo_stock_full_permission",
                        "gateway_full_permission",
                        IdentityServerConstants.LocalApi.ScopeName
                    }
                },
                new Client()
                {
                    ClientName = "Asp.Net Core Mvc",
                    ClientId = "WebMvcClientForUser",
                    AllowOfflineAccess = true,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowedScopes =
                    {
                        "basket_full_permission",
                        "order_full_permission",
                        "gateway_full_permission",
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        "roles",
                        IdentityServerConstants.LocalApi.ScopeName
                    },
                    AccessTokenLifetime = 1 * 60 * 60, // default 1 saattir
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime = (int) (DateTime.Now.AddDays(60) - DateTime.Now).TotalSeconds,
                    RefreshTokenUsage = TokenUsage.ReUse,
                },
                new Client()
                {
                    ClientName = "Token Exchange Client",
                    ClientId = "TokenExchangeClient",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedGrantTypes = new []{"urn:ietf:params:oauth:grant-type:token-exchange"},
                    AllowedScopes =
                    {
                        "discount_full_permission",
                        "payment_full_permission",
                        IdentityServerConstants.StandardScopes.OpenId
                    }
                },
            };
    }
}