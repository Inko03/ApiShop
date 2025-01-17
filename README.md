<h1 align="center">
ApiShop - a REST API written in .NET for an online shop.
It is still in development
<br>
</h1>

## Technologies used:
* .NET 8.0
* EntityFrameworkCore 9.0
* JwtBearer 8.0


## There are 4 controllers where you can access:
* [CartController](#CartController)
* [OrderController](#OrderController)
* [ProductController](#ProductController)
* [UserController](#UserController)

## UserController
* endpoint at  [/users](#users) | Method: POST
* it register user to database


- expect the following format data:
  
![image](https://github.com/user-attachments/assets/2d34117c-0f2f-4f24-83d6-c2eabe09eb4d)

If any error occurs, it will return only a message to you. The possible status is:
* 200 Ok
* 400 Bad request 
* 401 Unauthorized

