import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

const apiEndpoint = "https://localhost:7173/api/Shipper/register";

function AddShipper() {
  const navigate = useNavigate();
  const [account, setAccount] = useState({
    email: "",
    username: "",
    fullName: "",
    address: "",
    phoneNumber: "",
    password: "",
    confirmPassword: "",
  });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setAccount((prevState) => ({ ...prevState, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (account.password !== account.confirmPassword) {
      alert("Passwords do not match!");
      return;
    }

    try {
      const response = await axios.post(apiEndpoint, {
        email: account.email,
        username: account.username,
        fullName: account.fullName,
        address: account.address,
        phoneNumber: account.phoneNumber,
        password: account.password,
        confirmPassword: account.confirmPassword,
      });
      if (response.data.isSuccess) {
        navigate("/dashboard/shipper");
      } else {
        alert("Failed to add shipper: " + response.data.message);
      }
    } catch (error) {
      console.error("Error adding new shipper:", error);
      alert("Error adding new shipper: " + error.message);
    }
  };

  return (
    <div className="container mt-3">
      <h2>Add New Shipper</h2>
      <form onSubmit={handleSubmit} className="mb-3">
        <div className="mb-3">
          <label className="form-label">Email</label>
          <input
            type="email"
            className="form-control"
            name="email"
            value={account.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Username</label>
          <input
            type="text"
            className="form-control"
            name="username"
            value={account.username}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Full Name</label>
          <input
            type="text"
            className="form-control"
            name="fullName"
            value={account.fullName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Address</label>
          <input
            type="text"
            className="form-control"
            name="address"
            value={account.address}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Phone Number</label>
          <input
            type="text"
            className="form-control"
            name="phoneNumber"
            value={account.phoneNumber}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Password</label>
          <input
            type="password"
            className="form-control"
            name="password"
            value={account.password}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label className="form-label">Confirm Password</label>
          <input
            type="password"
            className="form-control"
            name="confirmPassword"
            value={account.confirmPassword}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary">
          Save
        </button>
        <button
          type="button"
          className="btn btn-secondary ms-2"
          onClick={() => navigate("/shipper-account-management")}
        >
          Cancel
        </button>
      </form>
    </div>
  );
}

export default AddShipper;
