# ThAmCo Events Web Application

## Project Overview

This project involves the development of an intranet-based event management system for **ThAmCo** (Three Amigos Corp), an event management company. The system enables staff to manage event details, guests, food orders, and staffing. The application is developed using **ASP.NET Core** and **EntityFramework Core**, adhering to the functional and technical requirements outlined in the module specification.

## System Architecture

The system is divided into three core components:

### ThAmCo.Events Web Application

- Enables management of customers, staff, and events.
- Developed using **ASP.NET Core RAZOR PAGES**.

### ThAmCo.Venues Web Service

- Provides venue querying and booking capabilities.
- Pre-built component; modifications are not permitted.

### ThAmCo.Catering Web Service

- Manages food orders, menus, and food bookings for events.
- Developed as an **ASP.NET API web service**.

The application integrates these components to deliver a cohesive event management solution.

## Functional Requirements

### **MUST** Requirements

#### **ThAmCo.Catering Web Service:**

- Create, edit, delete, and list food items.
- Create, edit, delete, and list food menus.
- Add or remove food items from menus.
- Book, edit, and cancel food for events, returning a **FoodBookingId** upon successful booking.

#### **ThAmCo.Events Web Application:**

- Create, list, and edit guests.
- Create events with essential details (**title**, **date**, and **type**).
- Edit event details, excluding date and type.
- Book guests for events and list guests with attendance tracking.
- View detailed guest information, including associated events and attendance.

### **SHOULD** Requirements

- Cancel guest bookings for upcoming events.
- Reserve or free venues for events using the **ThAmCo.Venues** service.
- Display a summary of events, including guest and venue information.
- Manage staff details and assign staffing to events.
- Show warnings for events without a first aider.

### **WOULD** Requirements

- Display detailed event information, including venues, staff, and guests.
- Permanently anonymize guest data upon request.
- Filter and display available venues by event type and date range.
- Ensure staffing compliance (e.g., at least one staff member per 10 guests).

## Development Notes

### Frameworks and Tools:

- **ASP.NET Core 6.0** for web and API services.
- **EntityFramework Core** for data management.
- **MSSQL** for database backend.

### Customizations:

- Extend and customize UI/UX beyond scaffolded templates.
- Implement security measures to restrict sensitive operations (e.g., staffing adjustments).

### Code Design:

- Follow layered architecture with separation of concerns (presentation, business logic, data access).
- Utilize design patterns and refactor code for clarity and maintainability.

## Data Model

The data model is designed to support key operations:

### **ThAmCo.Events:**

- Entities: **Event**, **Guest**, **Staff**.
- Relationships: One-to-many between events and guests; events and staff.

### **ThAmCo.Catering:**

- Entities: **Menu**, **FoodItem**, **FoodBooking**.
- Relationships: Many-to-many between menus and food items; one-to-many between events and food bookings.

## Testing and Documentation

- Develop a comprehensive test plan covering all functional requirements.
- Include code comments and alternative solutions in a README or inline documentation.
- Capture test results and document whether each functional requirement is met.

## Security Considerations

- Implement role-based access control for sensitive operations.
- Plan for data protection, including anonymization features.
- Secure communication between services using HTTPS.

## Setup Instructions

1. Clone the repository.
2. Ensure you have **SQL Server** installed on your machine.
3. Configure the database connection strings in the **appsettings.json** file with the following connection string for a local instance:
4. You will have to configure mulitple startup projects
   - Right click solution -> Configure startup projects -> Select Multiple startup projects and then set the actions to "Start" for each project.
   - Save then run the newly created profile

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ThAmCo;Trusted_Connection=True;MultipleActiveResultSets=true"
}
