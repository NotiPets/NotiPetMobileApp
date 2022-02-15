# NotiPet Mobile App

## Sobre el Proyecto 🐾

NotiPet es un proyecto cuyo propósito es el de conectar a los dueños de mascotas con las veterinarias con los cuales contratan servicios para sus mascotas permitiéndoles a estos estar al tanto de los servicios que le están siendo realizados a sus mascotas para de esta forma estar al tanto de éstos en todo momento.

## Estructura de la solución

#### Proyecto en general

**[Domain-Driven Design (DDD)](https://en.m.wikipedia.org/wiki/Domain-driven_design)** es un enfoque de desarrollo de software para necesidades complejas que conecta la implementación con un modelo en evolución.  La premisa del diseño orientado al dominio es la siguiente:

- Situar el foco principal del proyecto en el dominio central y la lógica del dominio.
- Basar los diseños complejos en un modelo del dominio.
- Iniciar una colaboración creativa entre los expertos técnicos y los del dominio para perfeccionar de forma iterativa un modelo conceptual que aborde un dominio concreto.

| Proyecto                     | Descripción | 
| ---------------------------  |-------------| 
| NotiPet.Domain               | Modelos de negocio e interfaces                                         | 
| NotiPet.Data                 | Implementación de APIs Rest, DataSources y Repositorios y mapeo de DTOs | 
| NotiPet                      | Aplicación móvil                                                        | 
| NotiPet.iOS                  | Plataforma iOS                                                          | 
| NotiPet.Droid                | Plataforma Android                                                      | 
| NotiPet.Tests                | Pruebas unitarias                                                       |

#### UI 

| Namespace | Descripción |
|--------------|--------------|
| NotiPet.Behaviors | Comportamientos ampliados para los componentes de Xamarin.Forms |
| XNotiPet.Converters | Convertidores XAML, utilizados para convertir los datos de enlace de datos en algo que el XAML entienda |
| NotiPet.Effects | Efectos para aplicar cambios ligeros en los renderizadores de Xamarin.Forms |
| NotiPet.Extensions | Extensiones de marcado XAML para que XAML sea aún más funcional |
| NotiPet.Model | Cosas que tienen que ver con modelos y objetos, útil para tus necesidades de MVVM |
| NotiPet.Views | Controles como elementos ui reutilizables |

## Estrategia de Branching

1) Cree una rama de características a partir de la rama de clientes en la que va a trabajar. Utilizando la siguiente convención de nomenclatura:

      Name: **feature/{my initials}-{azure-dev-ops-task-id}-{feature name}**

      Example:

      **Developer Name**: Rafael Fernandez
            
      **Feature**: Appoiments Module
      
      **Azure DevOps Task**: 1234

      New Branch: feature/rf-1234-appoiments-module
      
       **Por favor, incluya también el ID de la tarea de Azure Dev Ops en cada confirmación**.

2) Una vez que la función se ha completado, se retira el branch.

3) Envíe un PR al branch main **(NUNCA A LA RAMA PRINCIPAL)**
      
      Naming Convention: **[customer-name][azure-dev-ops-task-id] - Feature Name**
      
      Example:
      
      PR Name : [rf][1234] - Appoiments Module

      PR source branch: feature/rf-1234-appoiments-module
      
      PR target branch: develop

3) Se puede fusionar con la rama del main una vez que se apruebe el PR

