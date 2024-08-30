import { useState, useEffect } from "react";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";

const apiEndpoint = "https://localhost:7173/api/User"; // Your updated API endpoint
const deleteApiEndpoint = "https://localhost:7173/api/User/DeleteUser";

function useAdminAccountManagement() {
  const [accounts, setAccounts] = useState([]);
  const [selectedAccount, setSelectedAccount] = useState(null);

  useEffect(() => {
    fetchAccounts();
  }, []);

  const fetchAccounts = async () => {
    try {
      const { data } = await axios.get(apiEndpoint);
      if (data.isSuccess && data.data) {
        setAccounts(data.data);
      } else {
        console.error("Unexpected API response format:", data);
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
        fetchAccounts();
      } catch (error) {
        console.error("Error disabling account:", error);
      }
    }
  };

  return {
    accounts,
    selectedAccount,
    fetchAccounts,
    handleDisable,
    setSelectedAccount,
  };
}

export default useAdminAccountManagement;
