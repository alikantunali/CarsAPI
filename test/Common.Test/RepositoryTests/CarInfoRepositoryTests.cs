﻿using Common.Entities;
using Common.Models;
using Common.Repositories.CarListService;
using Moq;


namespace Common.Test.RepositoryTests
{
    public class CarInfoRepositoryTests
    {
        private List<Car> list;        
        private CarInfoRepository repository;
        public CarInfoRepositoryTests()
        {
            list = CarsDataStore.cars.ToList();
            repository = new CarInfoRepository();
        }

        [Fact]
        public void GetAllCars_Returns_ListOfCars()
        {
            //ARRANGE
            var cars = list;
            
            //ACT
            var result = repository.GetCarsFromList();

            //ASSERT
            Assert.NotNull(result);
            Assert.Equal(cars,result.Result);
        }

        [Fact]
        public void GetCarbyId_Returns_ACar()
        {
            //ARRANGE
            var cars = list;
            var carId= cars.FirstOrDefault().Id;

            //ACT
            var result = repository.GetCarFromList(carId);

            //ASSERT
            Assert.NotNull(result);
            Assert.IsType<Task<Car>>(result);            
        }

        [Fact]
        public void GetCarbyId_ReturnsException_WhenIdIsZero()
        {
            //ARRANGE

            //ACT
            var result = repository.GetCarFromList(It.IsAny<int>());
            var exception = result.Exception.InnerException;

            //ASSERT             
            Assert.IsType<ArgumentOutOfRangeException>(exception);                    
        }

        [Fact]
        public void GetCarbyId_ReturnsException_WhenIdIsNotFound()
        {
            //ARRANGE
            var idOutOfRange = list.Count() + 1;

            //ACT
            var result = repository.GetCarFromList(idOutOfRange);
            var exception = result.Exception.InnerException;

            //ASSERT             
            Assert.IsType<Exception>(exception);
            Assert.Equal("No such car with the given Id.", exception.Message);
        }

        [Fact]
        public void AddCar_ReturnsListofCars_WhenIdIsGreaterThan3()
        {
            //ARRANGE
            var car = list.First();
            car.ManufactureYear = "2022";
            car.Id = 4;

            //ACT
            var result = repository.AddCarToList(car);

            //ASSERT             
            Assert.NotNull(result);
            Assert.IsType<Task<List<Car>>>(result);           
        }

        [Fact]
        public void DeleteCar_ReturnsException_WhenCarDoesNotExist()
        {
            //ARRANGE

            //ACT
            var result = repository.DeleteCarFromList(It.IsInRange<int>(1,3,Moq.Range.Inclusive));

            var exception = result.Exception.InnerException;

            //ASSERT             
            Assert.IsType<ArgumentOutOfRangeException>(exception);
            Assert.Equal("Specified argument was out of the range of valid values. (Parameter 'carId')", exception.Message);
        }

        [Fact]
        public void DeleteCar_ReturnsUpdatedCarList_WhenCarIsDeleted()
        {
            //ARRANGE
            var car = list.First();

            //ACT
            var result = repository.DeleteCarFromList(car.Id);

            //ASSERT             
            Assert.IsType<Task<List<Car>>>(result);
            
        }
        [Fact]
        public void AddCar_ReturnsException_WhenIdIsLessThan3()
        {
            //ARRANGE
            var fakelist = FakeData.CarMockData.GetData();
            var car = fakelist.First();

            //ACT
            var result = repository.AddCarToList(car);
            var exceptionType = result.Exception;
            var exceptionMsg = exceptionType?.InnerException?.Message;

            //ASSERT             
            Assert.IsType<AggregateException>(exceptionType);
            Assert.Equal("Check car properties. Id must be greater than 3.", exceptionMsg);
        }


    }
}
