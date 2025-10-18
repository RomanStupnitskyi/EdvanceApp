using System.Diagnostics.CodeAnalysis;
using NetArchTest.Rules;
using Shouldly;

namespace Architecture.Tests.Layers;

[SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores")]
public class LayerTests : BaseTest
{
    [Fact]
    public void Domain_Should_NotHaveDependencyOnApplication()
    {
        // Act
        TestResult? testResult = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult();

        // Assert
        testResult.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        // Act
        TestResult? testResult = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().FullName)
            .GetResult();

        // Assert
        testResult.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void DomainLayer_ShouldNotHaveDependencyOn_WebApiLayer()
    {
        // Act
        TestResult? testResult = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        // Assert
        testResult.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_InfrastructureLayer()
    {
        TestResult? result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void ApplicationLayer_ShouldNotHaveDependencyOn_WebApiLayer()
    {
        TestResult? result = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    [SuppressMessage("Globalization", "CA1307:Specify StringComparison for clarity")]
    public void Handlers_Should_Have_DependencyOnDomain()
    {
        TestResult? testResult = Types
            .InAssembly(ApplicationAssembly)
            .That()
            .HaveNameEndingWith("Handler")
            .Should()
            .HaveDependencyOn(DomainAssembly.GetName().Name)
            .GetResult();
        
        // Assert
        testResult.IsSuccessful.ShouldBeTrue();
    }
    
    [Fact]
    public void InfrastructureLayer_ShouldNotHaveDependencyOn_WebApiLayer()
    {
        TestResult? result = Types.InAssembly(InfrastructureAssembly)
            .Should()
            .NotHaveDependencyOn(WebApiAssembly.GetName().Name)
            .GetResult();

        result.IsSuccessful.ShouldBeTrue();
    }
}