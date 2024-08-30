import {
  BsFillArchiveFill,
  BsFillGrid3X3GapFill,
  BsPeopleFill,
} from "react-icons/bs";
import {
  BarChart,
  Bar,
  Rectangle,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
  LineChart,
  Line,
  Pie,
  PieChart,
  Cell,
} from "recharts";
import { FaSackDollar } from "react-icons/fa6";
import "./report.scss";
import { useEffect, useState } from "react";
import axios from "axios";
import { EffectCards } from "swiper/modules";
import { TruckOutlined } from "@ant-design/icons";

function Report() {
  //Food
  const [dataFood, setDateFood] = useState(0);
  const fetchFood = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/total-food-menu"
      );
      console.log(response.data);
      setDateFood(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchFood();
  }, []);
  //Category
  const [dataCategories, setDateCategories] = useState(0);
  const fetchCategories = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/total-categories"
      );
      console.log(response.data);
      setDateCategories(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchCategories();
  }, []);
  //Chart
  const [data, setData] = useState([]);
  const fetchChart = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/GetTopFiveCustomer"
      );
      console.log(response.data);
      setData(response.data);
    } catch (e) {
      console.log(e);
    }
  };
  useEffect(() => {
    fetchChart();
  }, []);
  //Order
  const [dataOrder, setDateOrder] = useState(0);
  const fetchOrder = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/total-orders"
      );
      console.log(response.data);
      setDateOrder(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchOrder();
  }, []);

  //User
  const [dataUser, setDataUser] = useState(0);
  const fetchUser = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/AdminDashboard/dashboard/active-user"
      );
      console.log(response.data);
      setDataUser(response.data);
    } catch (e) {
      console.log(e);
    }
  };

  useEffect(() => {
    fetchUser();
  }, []);

  // const data = [
  //   { name: "Page A", pv: 2400 },
  //   { name: "Page B", pv: 1398 },
  //   { name: "Page C", uv: 2000 },
  //   { name: "Page D", uv: 2780 },
  // ];
  const renderCustomizedLabel = ({
    cx,
    cy,
    midAngle,
    innerRadius,
    outerRadius,
    percent,
    index,
  }) => {
    const radius = innerRadius + (outerRadius - innerRadius) * 0.5;
    const x = cx + radius * Math.cos(-midAngle * RADIAN);
    const y = cy + radius * Math.sin(-midAngle * RADIAN);

    return (
      <text
        x={x}
        y={y}
        fill="white"
        textAnchor={x > cx ? "start" : "end"}
        dominantBaseline="central"
      >
        {`${(percent * 100).toFixed(0)}%`}
      </text>
    );
  };
  const COLORS = ["#0088FE", "#00C49F", "#FFBB28", "#FF8042"];

  return (
    <main className="main-container-report">
      <div className="main-title-report">
        <h3>STATISTICS</h3>
      </div>
      <div className="main-cards-report">
        <div className="card-report">
          <div className="card-inner-report">
            <BsFillArchiveFill className="card_icon-report" />
            <h3>Product</h3>
            <h1>{dataFood}</h1>
          </div>
        </div>
        <div className="card-report">
          <div className="card-inner-report">
            <BsFillGrid3X3GapFill className="card_icon-report" />
            <h3>CATEGORIES</h3>
            <h1>{dataCategories}</h1>
          </div>
        </div>
        <div className="card-report">
          <div className="card-inner-report">
            <BsPeopleFill className="card_icon-report" />
            <h3>CUSTOMERS</h3>
            <h1>{dataUser}</h1>
          </div>
        </div>
        <div className="card-report">
          <div className="card-inner-report">
            <TruckOutlined className="card_icon-report" />
            <h3>TotalOrder</h3>
            <h1>{dataOrder}</h1>
          </div>
        </div>
      </div>

      <div className="charts-report w-full ">
        <ResponsiveContainer width="100%" height="100%" className="mx-auto">
          <BarChart
            width={1000}
            height={300}
            data={data}
            margin={{
              top: 5,
              right: 30,
              left: 20,
              bottom: 5,
            }}
          >
            <CartesianGrid strokeDasharray="3 3" />
            <XAxis dataKey="customerName" />
            <YAxis />
            <Tooltip />
            <Legend />
            <Bar dataKey="totalOrders" fill="#8884d8" />
          </BarChart>
        </ResponsiveContainer>
      </div>
    </main>
  );
}

export default Report;
