using Microsoft.VisualStudio.TestTools.UnitTesting;
using EvaluationSampleCode;

namespace EvaluationSampleCode.Tests
{
  [TestClass]
  public class ReservationTests
  {
    private User _regularUser;
    private User _adminUser;
    private User _anotherUser;

    [TestInitialize]
    public void Setup()
    {
      _regularUser = new User { IsAdmin = false };
      _adminUser = new User { IsAdmin = true };
      _anotherUser = new User { IsAdmin = false };
    }

    #region Constructor Tests
    [TestMethod]
    public void Constructor_ValidUser_SetsUserCorrectly()
    {
      var reservation = new Reservation(_regularUser);

      Assert.AreEqual(_regularUser, reservation.MadeBy);
    }

    [TestMethod]
    public void Constructor_AdminUser_SetsUserCorrectly()
    {
      var reservation = new Reservation(_adminUser);

      Assert.AreEqual(_adminUser, reservation.MadeBy);
    }

    [TestMethod]
    public void Constructor_NullUser_SetsUserToNull()
    {
      var reservation = new Reservation(null);

      Assert.IsNull(reservation.MadeBy);
    }
    #endregion

    #region CanBeCancelledBy Tests - Regular User Scenarios
    [TestMethod]
    public void CanBeCancelledBy_SameUserWhoMadeReservation_ReturnsTrue()
    {
      var reservation = new Reservation(_regularUser);

      var result = reservation.CanBeCancelledBy(_regularUser);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_DifferentRegularUser_ReturnsFalse()
    {
      var reservation = new Reservation(_regularUser);

      var result = reservation.CanBeCancelledBy(_anotherUser);

      Assert.IsFalse(result);
    }
    #endregion

    #region CanBeCancelledBy Tests - Admin User Scenarios
    [TestMethod]
    public void CanBeCancelledBy_AdminUser_ReturnsTrue()
    {
      var reservation = new Reservation(_regularUser);

      var result = reservation.CanBeCancelledBy(_adminUser);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_AdminUserOnOwnReservation_ReturnsTrue()
    {
      var reservation = new Reservation(_adminUser);

      var result = reservation.CanBeCancelledBy(_adminUser);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_AdminUserOnAnotherReservation_ReturnsTrue()
    {
      var reservation = new Reservation(_regularUser);

      var result = reservation.CanBeCancelledBy(_adminUser);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_AdminUserOnAnotherAdminReservation_ReturnsTrue()
    {
      var anotherAdmin = new User { IsAdmin = true };
      var reservation = new Reservation(anotherAdmin);

      var result = reservation.CanBeCancelledBy(_adminUser);

      Assert.IsTrue(result);
    }
    #endregion

    #region CanBeCancelledBy Tests - Edge Cases
    [TestMethod]
    public void CanBeCancelledBy_NullUser_ReturnsFalse()
    {
      var reservation = new Reservation(_regularUser);

      var result = reservation.CanBeCancelledBy(null);

      Assert.IsFalse(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_NullUserOnNullReservation_ReturnsFalse()
    {
      var reservation = new Reservation(null);

      var result = reservation.CanBeCancelledBy(null);

      Assert.IsFalse(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_RegularUserOnNullReservation_ReturnsFalse()
    {
      var reservation = new Reservation(null);

      var result = reservation.CanBeCancelledBy(_regularUser);

      Assert.IsFalse(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_AdminUserOnNullReservation_ReturnsTrue()
    {
      var reservation = new Reservation(null);

      var result = reservation.CanBeCancelledBy(_adminUser);

      Assert.IsTrue(result);
    }
    #endregion

    #region User Object Equality Tests
    [TestMethod]
    public void CanBeCancelledBy_SameUserObjectReference_ReturnsTrue()
    {
      var user = new User { IsAdmin = false };
      var reservation = new Reservation(user);

      var result = reservation.CanBeCancelledBy(user);

      Assert.IsTrue(result);
    }

    [TestMethod]
    public void CanBeCancelledBy_DifferentUserObjectsWithSameProperties_ReturnsFalse()
    {
      var user1 = new User { IsAdmin = false };
      var user2 = new User { IsAdmin = false };
      var reservation = new Reservation(user1);

      var result = reservation.CanBeCancelledBy(user2);

    }
    #endregion

    #region Integration Tests
    [TestMethod]
    public void Reservation_ComplexScenario_CancellationLogicWorksCorrectly()
    {
      var user1 = new User { IsAdmin = false };
      var user2 = new User { IsAdmin = false };
      var admin = new User { IsAdmin = true };

      var reservation1 = new Reservation(user1);
      var reservation2 = new Reservation(user2);
      var reservation3 = new Reservation(admin);

      Assert.IsTrue(reservation1.CanBeCancelledBy(user1));
      Assert.IsTrue(reservation2.CanBeCancelledBy(user2));
      Assert.IsTrue(reservation3.CanBeCancelledBy(admin));

      Assert.IsFalse(reservation1.CanBeCancelledBy(user2));
      Assert.IsFalse(reservation2.CanBeCancelledBy(user1));

      Assert.IsTrue(reservation1.CanBeCancelledBy(admin));
      Assert.IsTrue(reservation2.CanBeCancelledBy(admin));
      Assert.IsTrue(reservation3.CanBeCancelledBy(admin));
    }

    [TestMethod]
    public void Reservation_PropertyAccess_MadeByPropertyWorkCorrectly()
    {
      var user1 = new User { IsAdmin = false };
      var user2 = new User { IsAdmin = true };

      var reservation = new Reservation(user1);
      Assert.AreEqual(user1, reservation.MadeBy);

      reservation.MadeBy = user2;
      Assert.AreEqual(user2, reservation.MadeBy);

      Assert.IsTrue(reservation.CanBeCancelledBy(user2));
      Assert.IsFalse(reservation.CanBeCancelledBy(user1));
    }
    #endregion
  }
}