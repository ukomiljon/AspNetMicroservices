# Asp Net Microservices example.

Building Microservices on .Net which used Asp.Net Core Web API, MongoDB, CQRS (with Mediator Design Pattern including IPipelineBehavior for validation and exceptions), rabbitMQ message broker and Clean Architecture implementation

### Objective.
There are switch feature api and product api. To manage features of other api by switch feature. SwichFeature send message about feature status to the product api.
Product api stores it in memorydb and check for each reqquest wheather user has access to this command for products.

### How to run.

1. git clone 
2. open in visual studio or visual studio code and click debug button decker compose or
3. run  ```docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml up -d```
4. stop ```docker-compose -f .\docker-compose.yml -f .\docker-compose.override.yml down```
5. click this links to open api swagger

switch feature api: http://localhost:8000/swagger/index.html
product api: http://localhost:8001/swagger/index.html
 

### Steps to check
1. open switch feature api: http://localhost:8000/swagger/index.html
2.  To see all features

```
Request:
curl -X 'GET' \
  'http://localhost:8000/Feature' \
  -H 'accept: text/plain'  

Result:
 [
  "Create",
  "Delete",
  "GetAll",
  "Get"
]

```

3.  To enable Create Feature for abc@example.com user

```
Request:
curl -X 'POST' \
  'http://localhost:8000/Feature' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "featureName": "Create",
  "email": "abc@example.com",
  "enable": true
}' 

Result:
 status:200

```
4. if you hit step 2 again you will see  304 status of error.
   ```
   304	
   Error: Not Modified
   
   ```
5. Open  product api: http://localhost:8001/swagger/index.html  

6.  

 ```
 Request:
curl -X 'POST' \
  'http://localhost:8001/api/v1/Product' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "aaa",
  "skuCode": "bbb",
  "imageFile": "ccc",
  "price": 0,
  "email": "abc@example.com"
}'

Result: 204 success from server. you can get see the created product by name
```
7. go to switch feature api: http://localhost:8000/swagger/index.html, this time we change enable from true to false.

```
Request:
curl -X 'POST' \
  'http://localhost:8000/Feature' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "featureName": "Create",
  "email": "abc@example.com",
  "enable": false
}' 

Result:
 status:200

```

8. repeat step 7. you will see this error from server

```
code 500
System.NotSupportedException: Create product feature is not evailable.
```

TODO:
1. Disable/enable features automatically on UI using signalR without refreshing browser once feature switch service enables/disbales a feature.
2. Unit Tests
 
