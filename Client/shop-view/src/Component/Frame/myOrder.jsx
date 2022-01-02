import * as React from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { makeStyles } from '@material-ui/core';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import axios from 'axios';
import { config } from '../../api/config'

const useStyles = makeStyles(theme => ({
  context: {
    paddingTop: "2%",
    paddingLeft: "12%",
    paddingRight: "12%",
  },
  pageCenter: {
    margin: 'auto',
    paddingTop: "2%"
  }
}));

export default function OrderList(props) {
  const [expanded, setExpanded] = React.useState(false);
  const classes = useStyles();
  const userId = parseInt(localStorage.getItem("userId"));
  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  const [order, setOrder] = React.useState({
    OrderId: 0,
    Status: "",
    TotalComsume: 0
  })

  // const [commodity, setCommodity] = React.useState({
  //   Name: "",
  //   ImagePath: "",
  //   Price: 0,
  //   Count: 0,
  //   Descrite: ""
  // })

  //TEST
  const [commodity, setCommodity] = React.useState({
    Name: "滑鼠",
    ImagePath: "mouse",
    Price: 100,
    Count: 5,
    Descrite: "我是滑鼠"
  })

  React.useEffect(() => {
    axios.get('api/Order', config)
      .then(response => {
        console.log(response)
        setOrder(oldValues => ({
          ...oldValues,
          OrderId: response.data[0].orderId,
          Status: response.data[0].status,
          TotalConsume: response.data[0].totalConsume
        }));
      })
  }, [])

  return (
    <div className={classes.context}>
      <h1 style={{ 'display': 'inline-block' }}>訂單資訊</h1>
      <Accordion expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1bh-content"
          id="panel1bh-header"
        >
          <Typography sx={{ width: '33%', flexShrink: 0 }}>
            {`訂單編號 : ${order.OrderId}`}
          </Typography>
          <Typography sx={{ width: '33%', flexShrink: 0 }}>{`訂單狀態 : ${order.Status}`}</Typography>
          <Typography sx={{ width: '33%', flexShrink: 0 }}>{`訂單金額 : ${order.TotalConsume} 元`}</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            <div className={classes.commContext} >
              <img src={`https://localhost:44387/api/User/getImage/${commodity.ImagePath}`} style={{ "width": "150px", "height": "150px" }} />
            </div>
            <div className={classes.desc}>
              <div style={{ "border:": "2px #DCDCDC solid" }}>
                {`商品名稱 : ${commodity.Name}`}
              </div>
            </div>
            <div className={classes.desc}>
              <div style={{ "border:": "2px #DCDCDC solid" }}>
                {`購買數量 : ${commodity.Count}`}
              </div>
            </div>
            <div className={classes.desc}>
              <div style={{ "border:": "2px #DCDCDC solid" }}>
                {`商品總金額 : ${commodity.Count * commodity.Price}`}
              </div>
            </div>
            <div className={classes.desc}>
              <div style={{ "border:": "2px #DCDCDC solid" }}>
              {`商品描述 : ${commodity.Descrite}`}
              </div>
            </div>
          </Typography>
        </AccordionDetails>
      </Accordion>

      <Stack spacing={2}>
        <Pagination count={10} color="secondary" className={classes.pageCenter} />
      </Stack>
    </div>
  );
}
