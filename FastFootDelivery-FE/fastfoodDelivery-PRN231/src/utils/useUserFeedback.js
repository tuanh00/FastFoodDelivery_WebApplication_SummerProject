import { useState } from "react";
import axios from "axios";

const apiEndpoint = "https://localhost:7173/api/FeedBacks/ViewAllFeedBack";

function useUserFeedback() {
  const [feedbacks, setFeedbacks] = useState([]);

  const fetchFeedbacks = async () => {
    try {
      const { data } = await axios.get(apiEndpoint);
      if (data.isSuccess && data.data) {
        setFeedbacks(data.data);
      } else {
        console.error("Unexpected API response format:", data);
      }
    } catch (error) {
      console.error("Error fetching feedbacks:", error);
    }
  };

  return {
    feedbacks,
    fetchFeedbacks,
  };
}

export default useUserFeedback;
