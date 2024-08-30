import React, { useState } from "react";
import { Table, Button, Form } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import useAdminAccountManagement from "../../utils/useAdminAccountManagement";

function AdminAccountManagement() {
  const { accounts, handleDisable, setSelectedAccount } =
    useAdminAccountManagement();
  const [roleFilter, setRoleFilter] = useState("");
  const navigate = useNavigate();

  const filteredAccounts = roleFilter
    ? accounts.filter((account) => account.role === roleFilter)
    : accounts;

  return (
    <div className="container mt-3">
      <h2>Account Management</h2>
      <Form.Group controlId="roleFilter">
        <Form.Label>Filter by Role</Form.Label>
        <Form.Control
          as="select"
          value={roleFilter}
          onChange={(e) => setRoleFilter(e.target.value)}
        >
          <option value="">All Roles</option>
          <option value="Admin">Admin</option>
          <option value="User">User</option>
          <option value="Shipper">Shipper</option>
        </Form.Control>
      </Form.Group>
      <Button
        className="mt-2 mb-2"
        onClick={() => navigate("/dashboard/shipper")}
      >
        Manage Shipper Account
      </Button>
      <Button className="mt-2 mb-2" onClick={() => navigate("/view-feedback")}>
        View Feedback
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

export default AdminAccountManagement;
