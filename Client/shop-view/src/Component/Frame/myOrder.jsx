import * as React from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Typography from '@mui/material/Typography';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import Divider from '@material-ui/core/Divider';
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

  const [rows, setRows] = React.useState([
  ])

  React.useEffect(() => {
    var tempData = []
    axios.get('api/Order/GetOrderByUserId/' + userId, config)
      .then(response => {

        if (response.data.isSuccess) {
          response.data.message.forEach(r => {
            tempData.push(r)
          });
        }

        setRows(tempData);
      })
  }, [])

  let showResult = () => {
    var view = ""
    console.log(`rows = ${rows}`)
    if (rows.length != 0) {
      view = rows.map((row) => (
        <ExpansionPanel>
          <ExpansionPanelSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
          >
            <Typography sx={{ width: '33%', flexShrink: 0 }}>{`總價格 : ${row.price} 元`}</Typography>
          </ExpansionPanelSummary>
          <Divider />
          <ExpansionPanelDetails>
            <Typography>
              <div className={classes.commContext} >
                <img src={row.imagePath} style={{ "width": "150px", "height": "150px" }} />
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`商品名稱 : ${row.name}`}
                </div>
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`商品描述 : ${row.describe}`}
                </div>
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`數量 : ${1}`}
                </div>
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`商品金額 : ${1 * row.price}`}
                </div>
              </div>
            </Typography>
          </ExpansionPanelDetails>
        </ExpansionPanel>
      ))
    }
    else {
      view = <p>購買清單是空的喔! 快去購物吧!</p>
    }
    return view
  }


  return (
    <div className={classes.context}>
      <h1 style={{ 'display': 'inline-block' }}>訂單資訊</h1>
      {showResult()}
      <Stack spacing={2}>
        <Pagination count={1} color="secondary" className={classes.pageCenter} />
      </Stack>
    </div>
  );
}
