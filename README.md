# AuthSoft

AuthSoft is a project that provides authentication functionality using JWT tokens. It leverages MediatR for handling requests and FluentValidation for validating inputs.

## Features

- User registration
- User login
- User logout
- JWT token-based authentication

## Technologies

- **MediatR**: Used for handling requests and responses in a clean and decoupled manner.
- **FluentValidation**: Used for validating user inputs to ensure data integrity and consistency.
- **JWT Tokens**: Used for secure authentication and authorization.

## API Endpoints

### Register

- **Endpoint**: `/api/auth/register`
- **Method**: `POST`
- **Description**: Registers a new user.
- **Request Body**:
  ```json
  {
    "username": "string",
    "password": "string",
    "email": "string"
  }
  ```
- **Response**:
  ```json
  {
    "message": "User registered successfully."
  }
  ```

### Login

- **Endpoint**: `/api/auth/login`
- **Method**: `POST`
- **Description**: Authenticates a user and returns a JWT token.
- **Request Body**:
  ```json
  {
    "username": "string",
    "password": "string"
  }
  ```
- **Response**:
  ```json
  {
    "token": "string"
  }
  ```

### Logout

- **Endpoint**: `/api/auth/logout`
- **Method**: `POST`
- **Description**: Logs out the user by invalidating the JWT token.
- **Request Body**: None
- **Response**:
  ```json
  {
    "message": "User logged out successfully."
  }
  ```

### Refresh Token

- **Endpoint**: `/api/auth/refresh-token`
- **Method**: `POST`
- **Description**: Refreshes the JWT token using a refresh token.
- **Request Body**:
  ```json
  {
    "refreshToken": "string"
  }
  ```
- **Response**:
  ```json
  {
    "token": "string",
    "refreshToken": "string"
  }
  ```

## Getting Started

To get started with the project, follow these steps:

1. Clone the repository.
2. Install the required dependencies.
3. Configure the JWT settings in the `appsettings.json` file.
4. Run the application.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request for any changes.

## License

This project is licensed under the MIT License.
