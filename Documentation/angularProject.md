# Building Your First Angular Project

### 1. Create the Angular Project

- **Create the project**:  
  In your terminal, run:
  ```bash
  ng new project-name
  ```
  Replace `project-name` with the name of your application. Angular CLI will generate the basic structure for your project.

  - **Configuration**: During the setup, you’ll be prompted with a few options:
    - **Would you like to add Angular routing?**: Choose "Yes" if you want to enable routing (recommended if your app will have multiple pages or sections).
    - **Which stylesheet format would you like to use?**: Select your preferred style format (CSS, SCSS, etc.).

### 2. Start the Project on the Development Server

1. **Navigate to the project directory**:
   ```bash
   cd project-name
   ```

2. **Start the development server**:
   ```bash
   ng serve
   ```
   This will start the development server and compile the application, which will be available by default at `http://localhost:4200`.

3. **View the application**: Open `http://localhost:4200` in your browser to see your Angular app in action.

### 3. Project Structure

When you open the project in your code editor, you’ll see a structure similar to this:

```
project-name/
├── src/
│   ├── app/
│   │   ├── app.component.ts          # Root component
│   │   ├── app.module.ts             # Main application module
│   └── assets/                       # Static resources
│   └── environments/                 # Environment configurations
├── angular.json                      # Angular CLI configuration
├── package.json                      # Project dependencies
└── tsconfig.json                     # TypeScript configuration
```

Here, the `app/` directory contains the components of your application, where you’ll create new features.

### 4. Create a New Component

1. To add a new component, run:
   ```bash
   ng generate component component-name
   ```
   Replace `component-name` with the desired name for the component. This will automatically generate the component files and update references in the main module (`app.module.ts`).

2. **Use the new component**: In the `app.component.html` file, you can integrate your new component using its selector:
   ```html
   <app-component-name></app-component-name>
   ```

### 5. Compile and Prepare for Production

- **Build for production**: 
  ```bash
  ng build --prod
  ```
  This command generates a `dist/` folder with all files optimized for deployment to a production server.

And that’s it! With this, you have your basic Angular application created and configured, ready to be expanded and deployed.

### 6. Configuring Proxy for API Requests in Angular

In the Angular project configuration, specifically in the **angular.json** file, the following line should be added:

```json
"proxyConfig": "proxy.conf.cjs"
```

This line should be placed under the **"options"** section of the **"serve"** configuration for your project. The complete **"serve"** section will look like this:

```json
"serve": {
    "builder": "@angular-devkit/build-angular:dev-server",
    "configurations": {
        "production": {
            "buildTarget": "ClinicaDental:build:production"
        },
        "development": {
            "buildTarget": "ClinicaDental:build:development"
        }
    },
    "defaultConfiguration": "development",
    "options": {
        "proxyConfig": "proxy.conf.cjs"  // <-- Add this line here
    }
}
```

Including this configuration is essential for facilitating communication between the Angular application and a backend server during development. By configuring a proxy, it helps avoid CORS (Cross-Origin Resource Sharing) issues by allowing API requests to be correctly redirected to the appropriate server. This means that, when running the development server, any requests made to a defined endpoint (like `/api`) will be automatically redirected to the specified backend in the proxy file, thereby simplifying the development and testing process of the application.