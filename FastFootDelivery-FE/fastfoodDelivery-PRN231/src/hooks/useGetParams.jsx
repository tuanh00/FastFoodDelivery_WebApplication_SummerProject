import { useLocation } from "react-router-dom";

function useGetParams() {
  const location = useLocation();
  const getParams = (param) => {
    const data = new URLSearchParams(location.search);
    return data.get(param);
  };

  return getParams;
}

export default useGetParams;

// how to use
// const getParams = useGetParams();
// const myParam = getParams("id");
