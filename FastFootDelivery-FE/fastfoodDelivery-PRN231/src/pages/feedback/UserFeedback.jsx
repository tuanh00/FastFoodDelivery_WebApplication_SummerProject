import React, { useEffect } from "react";
import { Table } from "react-bootstrap";
import useUserFeedback from "../../utils/useUserFeedback";

function UserFeedback() {
  const { feedbacks, fetchFeedbacks } = useUserFeedback();

  useEffect(() => {
    fetchFeedbacks();
  }, []);

  return (
    <div className="container mt-3">
      <h2>User Feedback</h2>
      <h3 className="mt-5">All Feedbacks</h3>
      <Table striped bordered hover>
        <thead>
          <tr>
            <th>User Name</th>
            <th>Feedback</th>
          </tr>
        </thead>
        <tbody>
          {feedbacks.map((feedback) => (
            <tr key={feedback.feedBackId}>
              <td>{feedback.userName}</td>
              <td>{feedback.commentMsg}</td>
            </tr>
          ))}
        </tbody>
      </Table>
    </div>
  );
}

export default UserFeedback;
