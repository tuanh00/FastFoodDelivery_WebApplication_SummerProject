import React, { useState, useEffect } from "react";
import { Table, Button, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import useShipperAccountManagement from "../../utils/useShipperAccountManagement";

function ShipperAccountManagement() {
  const { accounts, handleDisable, fetchAccounts } =
    useShipperAccountManagement();
  const [roleFilter, setRoleFilter] = useState("Shipper");
  const navigate = useNavigate();

  useEffect(() => {
    fetchAccounts();
  }, [fetchAccounts]);

  const filteredAccounts = roleFilter
    ? accounts.filter((account) => account.role === roleFilter)
    : accounts;

  const handleAdd = () => {
    navigate("/dashboard/addshipper"); // Assuming '/add-shipper' is the route for adding a new shipper
  };

  return (
    <div className="container mt-3">
      <h2>Shipper Account Management</h2>
      <Button className="mt-2 mb-2" onClick={handleAdd}>
        Add New Shipper
      </Button>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>Full Name</th>
            <th>Username</th>
            <th>Role</th>
            <th>Phone Number</th>
            <th>Address</th>
            <th>Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {filteredAccounts.length === 0 ? (
            <tr>
              <td colSpan="7">No accounts found</td>
            </tr>
          ) : (
            filteredAccounts.map((account) => (
              <tr key={account.id}>
                <td>{account.fullName}</td>
                <td>{account.email}</td>
                <td>{account.role}</td>
                <td>{account.phoneNumber}</td>
                <td>{account.address}</td>
                <td>{account.status}</td>
                <td>
                  <Button
                    variant="danger"
                    onClick={() => handleDisable(account.id)}
                  >
                    Disable
                  </Button>
                </td>
              </tr>
            ))
          )}
        </tbody>
      </Table>
    </div>
  );
}

export default ShipperAccountManagement;
