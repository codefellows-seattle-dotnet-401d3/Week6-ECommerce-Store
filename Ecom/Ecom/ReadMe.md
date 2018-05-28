# ECommerce website
An assignment geared twoard the full production of an e-commerce store. The goal is to have a site that will allow the purchse of products based on role and claim as well as an admin console to adjust the products for the site. An email shoudl be sent out using SendGrid on registration and on order. I want to impliment OAuth for at least one source and take advanatge of Auth.net's sandbox account

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
- The current member roles are **Memeber** and **Admin** with admin granted access to effect products.
- The "Has guild" policy is intended to grant access to other sets of avalible products. *Not yet implimented*
- Individual class policies are also planned. *Only fighter policy is in place, others are not implimneted*


## OAUTH
The current OAuth is provided by google + *Full login from google intigrated but creating a new member account with it is not done*

## Email
- An email is sent when a user registers *logic in place but deployment issues are preventing a real test*
TODO An email is sent when an order is placed

## Azure Deployment
https://ecom20180509084450.azurewebsites.net
The user secrets are preventing this deployment from actually working.

My user secret file looks like this
https://docs.google.com/document/d/1jZjNZyZvFX93lOFTN0MjBJZ8_Aq7NUxgpeuGXgzuDqs/edit?usp=sharing

## Resources
