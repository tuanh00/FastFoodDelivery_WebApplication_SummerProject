import { Pie, PieChart, Cell } from "recharts";
import { Button, Form, Input, Select } from "antd";
import { FaSackDollar } from "react-icons/fa6";
import "./ReportRevenue.scss";
import { useEffect, useState } from "react";
import axios from "axios";

function ReportRevenue() {
  //Revenue
  const [dataRevenue, setDateRevenue] = useState(0);
  const fetchRevenue = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/GetTotalRevenue"
      );
      console.log(response.data);
      setDateRevenue(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchRevenue();
  }, []);
  // Revenue Monthly
  const [dataRevenueMonth, setDateRevenueMonth] = useState(0);

  const data = [
    { name: "Group A", value: 400 },
    { name: "Group B", value: 300 },
    { name: "Group C", value: 300 },
    { name: "Group D", value: 200 },
  ];
  const COLORS = ["#0088FE", "#00C49F", "#FFBB28", "#FF8042"];
  const onFinsh = async (value) => {
    console.log(value);
    const response = await axios.get(
      `https://localhost:7173/api/AdminDashboard/GetMonthlyRevenue?month=${value.month}&year=2024`
    );
    setDateRevenueMonth(response.data);
  };

  return (
    <main className="main-Revenue">
      <div className="main-InnerRevenue">
        <div className="card-Revenue">
          <div className="card-InnerRevenue">
            <FaSackDollar className="card_iconReve" />
            <h3>Revenue</h3>
            <h1>{dataRevenue}</h1>
          </div>
        </div>
      </div>

      <Form layout="inline" className="filter-form" onFinish={onFinsh}>
        <Form.Item label="Month" name="month">
          <Select
            options={[
              {
                label: "1",
                value: 1,
              },
              {
                label: "2",
                value: 2,
              },
              {
                label: "3",
                value: 3,
              },
              {
                label: "4",
                value: 4,
              },
              {
                label: "5",
                value: 5,
              },
              {
                label: "6",
                value: 6,
              },
              {
                label: "7",
                value: 7,
              },
              {
                label: "8",
                value: 8,
              },
              {
                label: "9",
                value: 9,
              },
              {
                label: "10",
                value: 10,
              },
              {
                label: "11",
                value: 11,
              },
              {
                label: "12",
                value: 12,
              },
            ]}
            placeholder="Enter month"
          />
        </Form.Item>
        <Form.Item>
          <Button type="primary" htmlType="submit">
            Filter
          </Button>
        </Form.Item>
      </Form>
      <div className="charts_Revenue w-[500px] ">
        <div className="earning-info">
          <h3>Earning</h3>
          <div>
            <p>${dataRevenueMonth}</p>
            <p>Monthly revenue</p>
          </div>
        </div>
        <PieChart width={230} height={230} className="chartz">
          <Pie
            data={data}
            cx="50%"
            cy="50%"
            innerRadius={60}
            outerRadius={80}
            fill="#8884d8"
            paddingAngle={5}
            dataKey="value"
          >
            {data.map((entry, index) => (
              <Cell
                key={`cell-${index}`}
                fill={COLORS[index % COLORS.length]}
              />
            ))}
          </Pie>
        </PieChart>
      </div>
    </main>
  );
}

export default ReportRevenue;
