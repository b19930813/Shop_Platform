import qs from 'qs'
import { useLocation, useHistory } from 'react-router-dom'
import React from "react";

const tryParseInt = value => {
  const result = parseInt(value)
  return isNaN(result) ? value : result
}

const parseObjectValues = (obj = {}) => {
  Object.keys(obj).forEach(k => {
    obj[k] = tryParseInt(obj[k])
  })

  return obj
}

const useQuery = () => {
    const { search } = useLocation();

    return React.useMemo(() => new URLSearchParams(search), [search]);
}

export default useQuery