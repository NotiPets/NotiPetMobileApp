# NotiPet Mobile App

## Sobre el Proyecto 馃惥

NotiPet es un proyecto cuyo prop贸sito es el de conectar a los due帽os de mascotas con las veterinarias con los cuales contratan servicios para sus mascotas permiti茅ndoles a estos estar al tanto de los servicios que le est谩n siendo realizados a sus mascotas para de esta forma estar al tanto de 茅stos en todo momento.

## Estructura de la soluci贸n

#### Proyecto en general

**[Domain-Driven Design (DDD)](https://en.m.wikipedia.org/wiki/Domain-driven_design)** es un enfoque de desarrollo de software para necesidades complejas que conecta la implementaci贸n con un modelo en evoluci贸n.  La premisa del dise帽o orientado al dominio es la siguiente:

- Situar el foco principal del proyecto en el dominio central y la l贸gica del dominio.
- Basar los dise帽os complejos en un modelo del dominio.
- Iniciar una colaboraci贸n creativa entre los expertos t茅cnicos y los del dominio para perfeccionar de forma iterativa un modelo conceptual que aborde un dominio concreto.

| Proyecto                     | Descripci贸n | 
| ---------------------------  |-------------| 
| NotiPet.Domain               | Modelos de negocio e interfaces                                         | 
| NotiPet.Data                 | Implementaci贸n de APIs Rest, DataSources y Repositorios y mapeo de DTOs | 
| NotiPet                      | Aplicaci贸n m贸vil                                                        | 
| NotiPet.iOS                  | Plataforma iOS                                                          | 
| NotiPet.Droid                | Plataforma Android                                                      | 
| NotiPet.Tests                | Pruebas unitarias                                                       |

#### UI 

| Namespace | Descripci贸n |
|--------------|--------------|
| NotiPet.Behaviors | Comportamientos ampliados para los componentes de Xamarin.Forms |
| XNotiPet.Converters | Convertidores XAML, utilizados para convertir los datos de enlace de datos en algo que el XAML entienda |
| NotiPet.Effects | Efectos para aplicar cambios ligeros en los renderizadores de Xamarin.Forms |
| NotiPet.Extensions | Extensiones de marcado XAML para que XAML sea a煤n m谩s funcional |
| NotiPet.Model | Cosas que tienen que ver con modelos y objetos, 煤til para tus necesidades de MVVM |
| NotiPet.Views | Controles como elementos ui reutilizables |

## Estrategia de Branching

1) Cree una rama de caracter铆sticas a partir de la rama de clientes en la que va a trabajar. Utilizando la siguiente convenci贸n de nomenclatura:

      Name: **feature/{my initials}-{azure-dev-ops-task-id}-{feature name}**

      Example:

      **Developer Name**: Rafael Fernandez
            
      **Feature**: Appoiments Module
      
      **Azure DevOps Task**: 1234

      New Branch: feature/rf-1234-appoiments-module
      
       **Por favor, incluya tambi茅n el ID de la tarea de Azure Dev Ops en cada confirmaci贸n**.

2) Una vez que la funci贸n se ha completado, se retira el branch.

3) Env铆e un PR al branch main **(NUNCA A LA RAMA PRINCIPAL)**
      
      Naming Convention: **[customer-name][azure-dev-ops-task-id] - Feature Name**
      
      Example:
      
      PR Name : [rf][1234] - Appoiments Module

      PR source branch: feature/rf-1234-appoiments-module
      
      PR target branch: develop

3) Se puede fusionar con la rama del main una vez que se apruebe el PR

