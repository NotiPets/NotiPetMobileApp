# NotiPet Mobile App

**[Domain-Driven Design (DDD)](https://en.m.wikipedia.org/wiki/Domain-driven_design)** is an approach to software development for complex needs by connecting the implementation to an evolving model.  The premise of Domain-Driven Design is the following:

- Placing the project's primary focus on the core domain and domain logic.
- Basing complex designs on a model of the domain.
- Initiating a creative collaboration between technical and domain experts to iteratively refine a conceptual model that addresses particular domain pr.

## Solution Structure

#### General Projects

| Projects                     | Description       | 
| ---------------------------  |-------------| 
| NotiPet.Domain               | Business models & interfaces | 
| NotiPet.Data                 | Rest APIs, DataSources and Repositories implementations & DTOs Mapping | 
| NotiPet                      | Mobile application | 
| NotiPet.iOS                  | iOS Platform  | 
| NotiPet.Droid                | Android Platform | 
| NotiPet.Tests                | Unit tests |

#### UI 

| Namespace | Description |
|--------------|--------------|
| NotiPet.Behaviors | Extended behaviors for the Xamarin.Forms components |
| XNotiPet.Converters | XAML converters, used to converted your data binding data into something your XAML understands |
| NotiPet.Effects | Effects to apply light-weight renderer changes to the Xamarin.Forms renderers |
| NotiPet.Extensions | XAML Markup Extensions to make your XAML even more functional |
| NotiPet.Model | Things that have to do with your models and objects. Probably handy for your MVVM needs |
| NotiPet.Views | Controls such as reusable ui elements |

## Branching Stratergy

1) Create a feature branch from the customer branch you will be working on. Using the following naming convention:

      Name: **feature/{my initials}-{azure-dev-ops-task-id}-{feature name}**

      Example:

      **Developer Name**: Rafael Fernandez
            
      **Feature**: Appoiments Module
      
      **Azure DevOps Task**: 1234

      New Branch: feature/rf-1234-appoiments-module
      
      **Please include the Azure Dev Ops Task ID in each commit as well**

2) Once feature is complete pull from the customer branch

3) Submit a PR to the customer branch **(NEVER TO master BRANCH)**
      
      Naming Convention: **[customer-name][azure-dev-ops-task-id] - Feature Name**
      
      Example:
      
      PR Name : [rf][1234] - Appoiments Module

      PR source branch: feature/rf-1234-appoiments-module
      
      PR target branch: develop

3) Can be merged to customer branch once PR is approved

