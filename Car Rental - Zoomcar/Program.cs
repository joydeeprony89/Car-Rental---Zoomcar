using System;
using System.Collections.Generic;
using System.Linq;

namespace Car_Rental___Zoomcar
{
  /*
  1. City
  2. Address
  3. Store
  3. Vehicle
  4. Reservation
  5. Bill
  6. Payment
  7. User

  Actors - Customer/Admin
   */
  class Program
  {
    static void Main(string[] args)
    {
      // add Users, cities, stores - test data setup
      var customers = GetCustomers();
      var cities = GetCity();
      VehicleBookingSystem system = new VehicleBookingSystem(cities, customers);

      // 1. Customer logged In
      var customer = system.Customers[0];
      // 2. customer select City
      var city = system.Cities[0];
      // 3. cutomer select store
      var store = city.GetStores()[0];
      store.AddVehicles(new List<Vehicle>());
      // 4 cutomer select vehicle
      var vehicle = store.GetVehicles()[0];
      // 5 book the vehicle at the store
      var reservation = store.CreateReservation(customer, vehicle);
    }

    static List<Customer> GetCustomers()
    {
      List<Customer> customers = new List<Customer>();
      customers.Add(new Customer("John", PersonStatus.Active));
      customers.Add(new Customer("Bob", PersonStatus.Active));

      return customers;
    }
    static List<City> GetCity()
    {
      List<City> cities = new List<City>();
      cities.Add(new City("1", "Bangalore"));
      cities.Add(new City("2", "Delhi"));

      return cities;
    }
  }

  public class VehicleBookingSystem
  {
    public List<City> Cities;
    public List<Customer> Customers;
    public VehicleBookingSystem(List<City> Cities, List<Customer> Customers)
    {
      this.Cities = Cities;
      this.Customers = Customers;
    }
  }

  public class Person
  {
    public  string name;
    public  string email;
    public  string phone;
  }
  public enum PersonStatus
  {
    Active,
    Inactive
  }

  public class Customer : Person
  {
    public  string id;
    public  PersonStatus PersonStatus;
    public Customer(string id, PersonStatus status)
    {
      this.id = id;
      PersonStatus = status;
    }
  }
  public class Admin : Person
  {
    public  string id;
    public  PersonStatus PersonStatus;
  }

  public class Address
  {
    public  string pincode;
    public  string streetname;
    public  string state;
  }
   
  public class City
  {
    public string cityCode;
    public string cityName;
    private List<Store> Stores;
    public City(string code, string name)
    {
      cityCode = code;
      cityName = name;
      Stores = new List<Store>();
    }

    public List<Store> GetStores()
    {
      Stores.Add(new Store("Whitefield"));
      Stores.Add(new Store("Brookfield"));

      return Stores;
    }
  }
  public class Store
  {
    public string storeId;
    public string storeName;
    public Address Address;
    public City City;

    public VehicleInventory VehicleInventory;
    public List<Reservation> Reservations;

    public Store(string name)
    {
      storeName = name;
      VehicleInventory = new VehicleInventory();
      Reservations = new List<Reservation>();
    }
    public void AddVehicles(List<Vehicle> vehicles)
    {
      vehicles.Add(new Vehicle(VehiecleType.Car));
      VehicleInventory.SetVehicles(vehicles);
    }

    public List<Vehicle> GetVehicles()
    {
      return VehicleInventory.Vehicles;
    }

    public Reservation CreateReservation(Customer customer, Vehicle vehicle) 
    {
      var reservation = new Reservation(customer, vehicle);
      return reservation;
    }
  }

  public enum VehiecleType
  {
    Car,
    Bike
  }

  public enum VehiecleStatus
  {
    Active,
    InActive
  }

  public class VehicleInventory
  {
    public List<Vehicle> Vehicles; 

    public void SetVehicles(List<Vehicle> vehicles)
    {
      Vehicles = vehicles;
    }
  }

  public class Vehicle
  {
    public string vehicleNo;
    public VehiecleType VehiecleType;
    public int noOfSeats;
    public double perDayCharge;
    double hourlyDayCharge;
    public VehiecleStatus VehiecleStatus;
    public Store Store;
    public bool isAvailable;
    public Vehicle(VehiecleType vehiecleType)
    {
      VehiecleType = vehiecleType;
    }
  }

  public class Car : Vehicle
  {
    public Car(): base(VehiecleType.Car)
    {

    }
  }

  public class Bike : Vehicle
  {
    public Bike() : base(VehiecleType.Bike)
    {

    }
  }

  public enum ReservationStatus
  {
    Canceled,
    Completed
  }
  public class Reservation
  {
    public string reservationNo;
    public Customer Customer;
    public Vehicle Vehicle;
    public DateTime reservationDate;
    public DateTime from;
    public DateTime to;
    public Bill Bill;
    public Reservation(Customer customer, Vehicle vehicle)
    {
      Customer = customer;
      Vehicle = vehicle;
    }
  }

  public class Bill
  {
    public string billNo;
    public bool isPaid;
    public double amount;

    public Payment Payment;
  }

  public enum PaymentStatus
  {
    Success,
    Failed,
  }

  public class Payment
  {
    public string paymentNo;
    public PaymentStatus PaymentStatus;
    public string paymentMode;
  }
}
