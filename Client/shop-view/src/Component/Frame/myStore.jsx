import * as React from 'react';
import ExpansionPanel from '@material-ui/core/ExpansionPanel';
import ExpansionPanelSummary from '@material-ui/core/ExpansionPanelSummary';
import ExpansionPanelDetails from '@material-ui/core/ExpansionPanelDetails';
import Divider from '@material-ui/core/Divider';
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
  },
  imageClass: {
    width: '200px',
    height: '200px',
    marginLeft: '50%',
    display: 'flex',
  },
  Text: {

  }
}));

function createData(Name, Desc, Count, Price, Image) {
  return { Name, Desc, Count, Price, Image };
}

export default function ControlledAccordions() {
  const [expanded, setExpanded] = React.useState(false);
  const classes = useStyles();
  const storeId = parseInt(localStorage.getItem("storeId"));
  const userName = localStorage.getItem("userName");
  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  const handleAddClick = () => {
    window.open('/AddCommodity')
  }

  const handleCreateStoreClick = () => {
    window.open('/CreateStore')
  }

  const [commodity, setCommodity] = React.useState({
    Name: "",
    Desc: "",
    Count: 0,
    Price: 0,
    Image: ""
  })

  const [rows, setRows] = React.useState([
  ])

  React.useEffect(() => {
    var tempData = []
    axios.get('api/Store/GetCommodityByStoreId/' + storeId, config)
      .then(response => {
        console.log(response)
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
      view = <p>商品是空的喔! 來賣東西吧!</p>
    }
    return view
  }


  return (
    <div className={classes.context}>
      <div>
        <h1 style={{ 'display': 'inline-block' }}>{`${userName}的商店`}</h1>
        {/* <Button color="warning" variant="contained" style={{ 'float': 'right', 'marginTop': '3%' }} onClick={handleCreateStoreClick} >建立賣場</Button> */}
        <Button color="warning" variant="contained" style={{ 'float': 'right', 'marginTop': '3%' }} onClick={handleAddClick} >加入商品</Button>
      </div>
      {showResult()}
      <Stack spacing={2}>
        <Pagination count={1} color="secondary" className={classes.pageCenter} />
      </Stack>
    </div>
  );
}
