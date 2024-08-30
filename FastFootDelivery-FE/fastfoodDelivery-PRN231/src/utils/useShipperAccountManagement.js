import { useState, useEffect } from "react";
import axios from "axios";
import { useNavigate } from "react-router-dom";

const apiEndpoint = "https://localhost:7173/api/Shipper";
const deleteApiEndpoint = "https://localhost:7173/api/Shipper/DeleteUser";

function useShipperAccountManagement() {
  const [accounts, setAccounts] = useState([]);
  const navigate = useNavigate();

  const fetchAccounts = async () => {
    try {
      const { data } = await axios.get(`${apiEndpoint}`);
      if (data.isSuccess) {
        setAccounts(data.data);
      } else {
        console.error("Failed to fetch accounts:", data.message);
      }
    } catch (error) {
      console.error("Error fetching accounts:", error);
    }
  };

  const handleDisable = async (id) => {
    const confirmed = window.confirm(
      "Are you sure you want to disable this account?"
    );
    if (confirmed) {
      try {
        await axios.delete(`${deleteApiEndpoint}?id=${id}`);
        fetchAccounts(); // Refresh the list after deletion
      } catch (error) {
        console.error("Error disabling account:", error);
      }
    }
  };

  return {
    accounts,
    fetchAccounts,
    handleDisable,
  };
}

export default useShipperAccountManagement;
