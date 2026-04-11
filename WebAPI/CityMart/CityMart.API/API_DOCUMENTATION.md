# CityMart E-Commerce API Documentation

## 📋 API Overview

This is a clean, well-structured e-commerce API built with:
- **Authentication**: JWT Tokens
- **Authorization**: Role-based (Admin, Customer)
- **Database**: SQL Server with Entity Framework Core
- **Framework**: ASP.NET Core 10

---

## 🔐 Authentication Flow

### 1. Register User
```
POST /api/auth/register
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "message": "User registered successfully",
  "userId": "user-guid-here"
}
```

---

### 2. Login & Get JWT Token
```
POST /api/auth/login
Content-Type: application/json

{
  "email": "user@example.com",
  "password": "password123"
}
```

**Response:**
```json
{
  "message": "Login successful",
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "userId": "user-guid-here",
  "email": "user@example.com",
  "userName": "user@example.com",
  "roles": ["Customer"]
}
```

**Usage of Token:**
Add the token to every request header as:
```
Authorization: Bearer <your-jwt-token>
```

---

## 📦 Public API Endpoints (No Authentication Required)

### Products - Browse Catalog

#### Get All Products
```
GET /api/products
```

**Response:**
```json
[
  {
    "id": 1,
    "name": "Laptop",
    "description": "Gaming Laptop",
    "price": 1500.00,
    "stock": 10
  }
]
```

#### Get Product by ID
```
GET /api/products/{id}
```

---

## 🛒 Customer API Endpoints (Requires Authentication)

### Cart Management

#### Get Current User's Cart
```
GET /api/carts
Authorization: Bearer <token>
```

#### Add Product to Cart
```
POST /api/carts/add
Authorization: Bearer <token>
Content-Type: application/json

{
  "productId": 1,
  "quantity": 2
}
```

#### Remove Item from Cart
```
DELETE /api/carts/remove/{cartItemId}
Authorization: Bearer <token>
```

#### Clear Entire Cart
```
DELETE /api/carts/clear
Authorization: Bearer <token>
```

---

### Orders

#### Get All User Orders
```
GET /api/orders
Authorization: Bearer <token>
```

#### Get Specific Order
```
GET /api/orders/{id}
Authorization: Bearer <token>
```

#### Checkout (Convert Cart to Order)
```
POST /api/orders/checkout
Authorization: Bearer <token>
```

**Response:**
```json
{
  "id": 1,
  "userId": "user-guid",
  "orderDate": "2026-03-20T10:30:00Z",
  "status": "Pending",
  "totalAmount": 3000.00,
  "items": [
    {
      "id": 1,
      "productId": 1,
      "quantity": 2,
      "price": 1500.00
    }
  ]
}
```

---

## 👨‍💼 Admin API Endpoints (Requires Admin Role)

### Product Management

#### Create Product
```
POST /api/admin/products
Authorization: Bearer <admin-token>
Content-Type: application/json

{
  "name": "Laptop",
  "description": "Gaming Laptop",
  "price": 1500.00,
  "stock": 10
}
```

#### Update Product
```
PUT /api/admin/products/{id}
Authorization: Bearer <admin-token>
Content-Type: application/json

{
  "name": "Updated Name",
  "description": "Updated Description",
  "price": 1600.00,
  "stock": 15
}
```

#### Delete Product
```
DELETE /api/admin/products/{id}
Authorization: Bearer <admin-token>
```

---

### Order Management

#### Get All Orders (All Users)
```
GET /api/admin/orders
Authorization: Bearer <admin-token>
```

#### Get Order Details
```
GET /api/admin/orders/{id}
Authorization: Bearer <admin-token>
```

#### Update Order Status
```
PUT /api/admin/orders/{id}/status
Authorization: Bearer <admin-token>
Content-Type: application/json

{
  "status": "Shipped"
}
```

**Valid Status Values:**
- `Pending`
- `Processing`
- `Shipped`
- `Delivered`
- `Cancelled`

---

## 🔑 JWT Token Configuration

Configured in `appsettings.json`:
```json
"JwtSettings": {
  "SecretKey": "your-super-secret-key-...",
  "Issuer": "CityMart.API",
  "Audience": "CityMart.Client",
  "ExpirationInHours": 24
}
```

⚠️ **Important**: Change the `SecretKey` in production!

---

## 📊 Data Models

### Product
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
```

### Order
```csharp
public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; } // Pending, Processing, Shipped, Delivered, Cancelled
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; }
}
```

### Cart
```csharp
public class Cart
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public List<CartItem> Items { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
```

---

## 🎯 API Design Principles

✅ **GET Endpoints**: Public, no authentication required  
✅ **POST/PUT/DELETE Endpoints**: Require authentication  
✅ **Admin Endpoints**: Require Admin role  
✅ **User Data Isolation**: Users can only access their own data  
✅ **Clean URLs**: Semantic routing with clear structure  

---

## 🚀 Testing in Postman

1. Register: `POST /api/auth/register`
2. Login: `POST /api/auth/login` → Copy token
3. Set Authorization header: `Authorization: Bearer {token}`
4. Test endpoints with token

---

## 📝 Error Responses

All errors follow a consistent format:
```json
{
  "message": "Error description",
  "errors": ["Additional error details"]
}
```

Common HTTP Status Codes:
- `200` - Success
- `201` - Created
- `400` - Bad Request
- `401` - Unauthorized
- `403` - Forbidden (Insufficient permissions)
- `404` - Not Found
- `500` - Server Error
