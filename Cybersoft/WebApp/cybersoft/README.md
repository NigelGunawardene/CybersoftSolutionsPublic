# Cybersoft

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 13.1.4.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.


Documentation for adding state management - 
npm install @ngrx/store@latest --legacy-peer-deps
npm install @ngrx/effects --save --legacy-peer-deps

Then import store module in our app module

Create state folder, inside create folders for each feature. create actions, reducer, selectors, effects, app state, and other feature states

When configuring the reducer in the appmodules, the names have to be the same of -
the name of the field in AuthState ("auth" in IAuthState for example) AND the name that is registered in the module file(StoreModule.forFeature('auth', AuthReducer),)

Can also configure in the app.module.ts file - 
StoreModule.forRoot({ auth: AuthReducer }),


After creating effects, we need to register it in the account.module.ts file - 
EffectsModule.forFeature([AuthEffects])

Also inside app.module.ts
EffectsModule.forRoot(),

-------------------------

After that, created the products related files
Registered like this -     
StoreModule.forRoot({ products: ProductsReducer}), //StoreModule.forRoot({ auth: AuthReducer }),
EffectsModule.forRoot([ProductsEffects]),

inside app.module.ts, since that is the module that the searchbarcomponent is declared




for devtools - 

npm install @ngrx/store-devtools --save

StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: false,
      autoPause: true,
      features: {
        pause: false,
        lock: true,
        persist: true
      }
    })


images compressed using https://tinypng.com/

imported ngrx entities to help manage arrays in store - 
ng add @ngrx/entity@latest

for npm packages - 
npm outdated
npm update --save/--save-dev - -this only updates to the latest possible in the defined range by ^ or ~

npm i -g npm-check-updates

run ncu
ncu -u


alt shift o - remove unused imports in code
alt shift F - autoformat


ctrl K and then D - autoformat in vs
ctrl k and then E - remove unused imports in vs


NEUMORPHISM UPDATES
Added assets/scss/neu file with variables

Imported that file in all angular component scss files and use them
Also had to modify route-animations.ts because the color of product cards are set using animations here