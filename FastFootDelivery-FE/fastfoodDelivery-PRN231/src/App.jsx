import {
  Navigate,
  RouterProvider,
  createBrowserRouter,
} from "react-router-dom";
import Layout from "./components/layout";
// import AdminAccountManagement from "./pages/accountuser-management";
import Category from "./pages/category-management";
import FoodItemManagement from "./pages/fastfood-magegement";
import HomePage from "./pages/home";
import Login from "./pages/login";
import PaymentFailure from "./pages/payment/paymentFail";
import PaymentSuccess from "./pages/payment/paymentSuccess";
import UserAccount from "./pages/profolio";
import Register from "./pages/register";
import ShoppingCart from "./pages/shoppingcart/ShoppingCart";
import UserFeedback from "././pages/feedback/UserFeedback";
import ShipperAccountManagement from "./pages/accountuser-management/ShipperAccountManagement";
import AddShipper from "./pages/accountuser-management/AddShipper";
import AdminAccountManagement from "./pages/accountuser-management/AdminAccountManagement";
import Dashboard from "./pages/dashboard-management";
import Report from "./pages/report";
import OrderAdmin from "./pages/orderAdmin";
import ReportRevenue from "./pages/reportRevenue";
import ViewOrderHistory from "./pages/accountuser-management/ViewOrderHistory";
import ViewShipperOrders from "./pages/accountuser-management/ViewShipperOrders";
import { useDispatch, useSelector } from "react-redux";
import { store } from "./redux/store";
import { toast } from "react-toastify";
import { logout } from "./redux/features/userAccount";
import ViewAllFeedback from "./pages/accountuser-management/ViewAllFeedBack";

function App() {
  const AdminRoute = ({ children, role }) => {
    const user = useSelector((store) => store.accountmanage);
    const dispatch = useDispatch();
    if (user?.role === role) {
      return children;
    } else {
      toast.error(" Access Denied");
      dispatch(logout());
      return <Navigate to="/login" />;
    }
  };

  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        {
          path: "/",
          element: <HomePage />,
        },
        // {
        //   path: "/fastfood-magegement",
        //   element: <FoodItemManagement />,
        // },
        // {
        //   path: "/category-management",
        //   element: <Category />,
        // },
        // {
        //   path: "/accountuser-management",
        //   element: <AdminAccountManagement />,
        // },
        {
          path: "/view-feedback",
          element: <UserFeedback />,
        },
        // {
        //   path: "/shipper-account-management",
        //   element: <ShipperAccountManagement />,
        // },
        // {
        //   path: "/addshipper",
        //   element: <AddShipper />,
        // },
        {
          path: "/login",
          element: <Login />,
        },
        {
          path: "/register",
          element: <Register />,
        },
        {
          path: "/shoppingcart",
          element: <ShoppingCart />,
        },
        {
          path: "/paymentSuccess",
          element: <PaymentSuccess />,
        },
        {
          path: "/paymentFail",
          element: <PaymentFailure />,
        },
        {
          path: "/profolio",
          element: <UserAccount />,
        },
        {
          path: "/viewOrderHistory",
          element: (
            <AdminRoute role="User">
              <ViewOrderHistory />
            </AdminRoute>
          ),
        },
        {
          path: "/viewshipperOrders",
          element: (
            <AdminRoute role="Shipper">
              <ViewShipperOrders />
            </AdminRoute>
          ),
        },
      ],
    },
    {
      path: "/dashboard",
      element: (
        <AdminRoute role="Admin">
          <Dashboard />
        </AdminRoute>
      ),
      children: [
        {
          path: "/dashboard/category",
          element: <Category />,
        },
        {
          path: "/dashboard/MenuFoodItem",
          element: <FoodItemManagement />,
        },
        {
          path: "/dashboard/OrderAdmin",
          element: <OrderAdmin />,
        },
        {
          path: "/dashboard/viewallfeedback",
          element: <ViewAllFeedback />,
        },
        {
          path: "/dashboard/accounts",
          element: <AdminAccountManagement />,
        },
        {
          path: "/dashboard/shipper",
          element: <ShipperAccountManagement />,
        },
        {
          path: "/dashboard/addshipper",
          element: <AddShipper />,
        },
        {
          path: "/dashboard/report",
          element: <Report />,
        },
        {
          path: "/dashboard/reportrevenue",
          element: <ReportRevenue />,
        },
      ],
    },
    // {
    //   path: "/profolio",
    //   element: <UserAccount />,
    // },
    // {
    //   path: "/",
    //   element: <HomePage />,
    // },
    // {
    //   path: "/fastfood-magegement",
    //   element: <FoodItemManagement />,
    // },
    // {
    //   path: "/login",
    //   element: <Login />,
    // },
  ]);

  return <RouterProvider router={router} />;
}

export default App;
