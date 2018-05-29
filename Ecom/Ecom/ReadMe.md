# ECommerce website
An assignment geared toward the full production of an e-commerce store. The goal is to have a site that will allow the purchase of products based on role and claim as well as an admin console to adjust the products for the site. An email should be sent out using SendGrid on registration and on order. I want to implement OAuth for at least one source and take advantage of Auth.net's sandbox account.

## Product
The focus on this site is selling a collection of gear for a set person

## Claims
Member roles
Name
Email
Guild
Class

## Policies
*What policies are you implementing? Why? Where are these policies being enforced?*
- The current member roles are **Member** and **Admin** with admin granted access to effect products.
- The "Has guild" policy is intended to grant access to other sets of available products. *Not yet implemented*
- Individual class policies are also planned. *Only fighter policy is in place, others are not implemented*

## OAUTH
The current OAuth is provided by google + *Full login from google integrated but creating a new member account with it is not done*

## Email
- An email is sent when a user registers *logic in place but deployment issues are preventing a real test*
TODO An email is sent when an order is placed

## Azure Deployment
https://ecom20180509084450.azurewebsites.net
The user secrets are preventing this deployment from actually working.

My user secret file looks like this
https://docs.google.com/document/d/1jZjNZyZvFX93lOFTN0MjBJZ8_Aq7NUxgpeuGXgzuDqs/edit?usp=sharing

## Resources
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/view-components?view=aspnetcore-2.1
https://blog.mariusschulz.com/2015/11/26/view-components-in-asp-net-mvc-6
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/policies?view=aspnetcore-2.1
https://docs.microsoft.com/en-us/aspnet/core/security/authorization/claims?view=aspnetcore-2.1
https://andrewlock.net/introduction-to-authentication-with-asp-net-core/
https://docs.microsoft.com/en-us/azure/sendgrid-dotnet-how-to-send-email
https://developer.authorize.net/hello_world/
https://hackerthemes.com/bootstrap-cheatsheet/