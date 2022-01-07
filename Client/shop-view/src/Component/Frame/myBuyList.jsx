import * as React from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { makeStyles } from '@material-ui/core';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
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

export default function BuyList() {
  const [expanded, setExpanded] = React.useState(false);
  const classes = useStyles();
  const userId = parseInt(localStorage.getItem("userId"));
  const userName = localStorage.getItem("userName");
  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  const [rows, setRows] = React.useState([
  ])

  React.useEffect(() => {
    axios.get('api/BuyList/GetBuyListByUserId/' + userId, config)
      .then(response => {
        console.log(response)
        var tempData = []

        response.data.forEach(r => {
          tempData.push(r)
        });
        setRows(tempData);
        console.log("tempData", tempData);
        console.log("rows", rows);
      })
  }, [])

  return (
    <div className={classes.context}>
      <h1>{`${userName}的購買清單`}</h1>
      {rows.map((row) => (
        <Accordion expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
          >
            <Typography sx={{ width: '33%', flexShrink: 0 }}>{`總價格 : ${row.price} 元`}</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              <div className={classes.commContext} >
                <img src={`https://localhost:44387/api/User/getImage/${row.ImagePath}`} style={{ "width": "150px", "height": "150px" }} />
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`商品名稱 : ${row.Name}`}
                </div>
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`商品描述 : ${row.Descrite}`}
                </div>
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`數量 : ${row.Count}`}
                </div>
              </div>
              <div className={classes.desc}>
                <div style={{ "border:": "2px #DCDCDC solid" }}>
                  {`商品金額 : ${row.Count * row.Price}`}
                </div>
              </div>
            </Typography>
          </AccordionDetails>
        </Accordion>
      ))}
      <Stack spacing={2}>
        <Pagination count={10} color="secondary" className={classes.pageCenter} />
      </Stack>
    </div>
  );
}
