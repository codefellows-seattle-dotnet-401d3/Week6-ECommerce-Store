# Week 6 Lab: Build an E-Commerce Store

## Overview
This is a multi-day assignment. Take the topics from each day of lecture, and apply them to your 
personal e-commerce store. Be sure to read the directions in it's entirety before beginning.

Pay close attention to the naming conventions specified. 

<hr />
<hr />

# Final Product Summary
Below are the final outcomes and responsibilities for each of the members within the site. 

**Total points for this assignment: 100pts**

## Anonymous User Functionality
1. View all products within the Ecommerce store
1. View individual products on their own landing pages

## Member Functionality
1. View their Profile and do the following:
	1. Edit personal information
	1. Change Password
	1. View Order History 
		- Just show the items and order total for each
1. Add items to their cart
1. Checkout their order
	- Upon checkout completion, their basket should clear/empty. 
1. View Receipt/Order Summary of submitted order (immediatly after checkout)

## Admin Functionality
1. Same abilites as a Member
1. Access to an Admin Dashboard
	1. Ability to Add/Update/Delete products
	1. View all users in the system
		- Change the role of certain users to promote them to "Admin"

## Readme:
Your Readme should clearly define the following:

### Product
What product are your selling?

### Claims
What claims are you assigning to your users, and where are they being assigned? Why are these claims important?

### Policies
What policies are you implementing? Why? Where are these policies being enforced?

### OAUTH
Who are your OAUTH providers?

### Email
Where are your emails being sent? What triggers them?

### Code Coverage:
What is your code coverage?

### Azure Deployment
What is your deployed link?



## Day 1 - Intro to Identity

* [ ] Create a brand new empty web application in Visual Studio. 
* [ ] Enable/Include the Identity Framework and all of it's required components. 
	* [ ] Enable Identity in the `ConfigureServices`, and add authentication to your `Configure` method
	* [ ] Create an `ApplicationUser` that derives from `IdentityUser`
	* [ ] Create an `ApplicationDbContext` that derives from IdentiyDbContext
	* [ ] Register the `ApplicationDbContext` in the `Startup.cs` File. 

* [ ] Create both a *Login* and *Register* page for your site. These actions should live in an *AccountController*.
	* [ ] Bring in `UserManager<ApplicationUser>` and `SignInManager<ApplicationUser>` into your controller through D.I. 

* [ ] start on creating the CSS/Frontend Design of your e-commerce site. Start on this early, as it will be a 
burnden to complete if you wait until the end of the project. 

## Day 2 - Claims and Application Roles
Adding onto your previous day's lab...
* [ ] Add default Application Roles (Admin and Member) to your application upon startup (seeding)
* [ ] Seed the Database with default products for your store (You will need to create a new DBContext, and register it separately in your application)
* [ ] Upon Registration add onto your user a variety of claims
* [ ] Assign users upon registration to a "MEMBER" user role. 
* [ ] Create a controller called *ProductsController* that allows you to do the standard CRUD operations on Products (you are allowed to scaffold, if you wish).
	* [ ] Make the Register and the Login Actions in the AccountController accessible by anonymous users
* [ ] Upon login, If the User is a member, redirect them to a Shopping landing page (you will have to create this)
* [ ] Upon login, If the user is an Admin, Redirect them to an Admin Dashboard that displays all of the products that are 
currently in the inventory. 

## Day 3 - Custom Policies
Adding onto previous day's lab, create 3 different types of policies based on what we went over in class.
You should have one each of the following:
* [ ] Role Based Policy (i.e. Only "Admins" can access a certain part of the site.)
* [ ] Claims based policies (You have to be 21+, you may NOT use a minimum age policy for your lab.)
* [ ] Present Claim based policy (Any user that has the "FavoriteColor" claim can access a part of the site)

### Additional Milestones
* [ ] Update your existing code so that only Admins have access to the CRUD operations of the product (You don't want your 
members to have the ability to manipulate inventory?!?!)
 
* [ ] Create a *ShopController* that is accessible by anyone (logged in or not) that shows all of the products that your
store has to offer. Display an image of the item, it's name, and the price. 

* [ ] Create a "Profile" action in your *AccountController* that displays the users information. 
	* [ ] Allow the user the ability to edit their personal information (*Not* their email address, as that is their username to login....
		- If you want to get tricky..there is a way to accomplish this..as a stretch goal you may figure it out if you wish.
	* [ ] Allow the user to change their password.


* [ ] Start thinking about how you will track user's adding items to their cart and order history of a user. 
	 - Can we make it so that an admin can view all order history as well?
	 - How are we going to track unchecked-out carts? (we will discuss this in class as different options...)

## Day 4

 Building off of previous day's lab....

 * [ ] Implement a `BasketDetails` view component into your site. This should live on your shopping pages(make sure it's on more than one page) to inform the users
 of all the items in their carts.
 * [ ] Make a Product landing page that is accessed whenever a user selects a product. This product landing page will show the product details, as well as 
 an option to "Add to cart". 

 ### Hints with BasketItems
 There are a couple different ways to hold onto your user's basket items. here are a couple of hints...
 1. Use an in session or in memory database
 2. Create seperate database tables such as `Baskets` and `BasketItems` that keeps track of the user's tables.
	- if you want another explanation on this, please ask me. 
 
## Day 5
Building off of previous day's lab...
* [ ] Incorporate at least 2 OAUTH external login providers into your site. 
* [ ] Be sure to capture the user's email if they choose to login through OAUTH provider

## Day 6
Use SendGrid to incorporate emails. You should have email notifications implemented for the followign scenarios:

* [ ] Email confirmation upon registration.
* [ ] Upon successful completion of an order, email the user a receipt. 
	- We have not setup a payment process yet, but this should not prevent you from adding an order confirmation process. 

	**Note** - Your email notifications should be an external action in a separate controller for usability sake. 
	Call this method/action with appropraite information whenever you want to prompt an email to be sent. Also, 
	Make sure that your emails are dynamic, and the 'to' is not hardcdoded. (it is ok for the From to be hardcoded)


In addition, pleaes add the following to your Account Profile:

* [ ] Ability to review previously submitted orders. 

## Day 7
Building off of Day 6....
* [ ] Complete your e-commerce site by creating a Sandbox account within Auth.Net.
* [ ] Integrate Auth.NET as your payment processor for your e-commerce site.
* [ ] Seperate out your payment processing into a model named `Payment`.
	- Consider creating a database table that stores all of the `Transactions` that take place within AUTH.NET
* [ ] Have a disclaimer on your checkout page that informs the user that this is a "fake" site, and that no real credit
card information should be entered. 
* [ ] Upon completion of the order, generate a receipt/order summary page for the user
* [ ] Save the Order history for each user, and allow them to view all previously submitted orders on their
MyAccount page. 
	- HINT: Create an OrderHistory table in the database that saves all the orders that have been created. 


## Final Milestones

* [ ] Code Coverage of 85%
* [ ] Web Site is Usable
* [ ] Front End design is "Client Ready" (it looks nice)
* [ ] Website is deployed to Azure
