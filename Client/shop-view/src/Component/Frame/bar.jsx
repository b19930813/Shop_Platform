import * as React from 'react';
import { styled, alpha } from '@mui/material/styles';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import { makeStyles } from '@material-ui/core/styles';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import InputBase from '@mui/material/InputBase';
import Badge from '@mui/material/Badge';
import MenuItem from '@mui/material/MenuItem';
import Menu from '@mui/material/Menu';
import MenuIcon from '@mui/icons-material/Menu';
import SearchIcon from '@mui/icons-material/Search';
import AccountCircle from '@mui/icons-material/AccountCircle';
import MailIcon from '@mui/icons-material/Mail';
import NotificationsIcon from '@mui/icons-material/Notifications';
import MoreIcon from '@mui/icons-material/MoreVert';
import Drawer from '@mui/material/Drawer';
import Divider from '@mui/material/Divider';
import ListItem from '@mui/material/ListItem';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';
import InboxIcon from '@mui/icons-material/MoveToInbox';
import List from '@mui/material/List';
import Button from '@mui/material/Button';
import SideList from './SideList'
import LoginForm from './LoginForm'
import Avatar from '@material-ui/core/Avatar';
import CssBaseline from '@material-ui/core/CssBaseline';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import Container from '@material-ui/core/Container';
import LockOutlinedIcon from '@material-ui/icons/LockOutlined';
import axios from 'axios';
import { config } from '../../api/config'
import FeedIcon from '@mui/icons-material/Feed';
import StoreIcon from '@mui/icons-material/Store';
import LocalGroceryStoreIcon from '@mui/icons-material/LocalGroceryStore';
import LocalOfferIcon from '@mui/icons-material/LocalOffer';
import PeopleIcon from '@mui/icons-material/People';
import MessageIcon from '@mui/icons-material/Message';

const useStyles = makeStyles(theme => ({

  paper: {
    marginTop: theme.spacing(2),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },

  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
  button: {
    border: 0,
    color: 'white',
    height: 60,
    padding: '0 30px',
  },
  icon: {
    fontSize: 20,
  },
  message: {
    display: 'flex',
    alignItems: 'center',
  },
subTitle: {
 marginTop: '2.7%',
 marginLeft: '0.5%',
 padding: "10%"
},
item: {
  padding: "10%"
}
}));



const Search = styled('div')(({ theme }) => ({
  position: 'relative',
  borderRadius: theme.shape.borderRadius,
  backgroundColor: alpha(theme.palette.common.white, 0.15),
  '&:hover': {
    backgroundColor: alpha(theme.palette.common.white, 0.25),
  },
  marginRight: theme.spacing(2),
  marginLeft: 0,
  width: '100%',
  [theme.breakpoints.up('sm')]: {
    marginLeft: theme.spacing(3),
    width: 'auto',
  },
}));



const SearchIconWrapper = styled('div')(({ theme }) => ({
  padding: theme.spacing(0, 2),
  height: '100%',
  position: 'absolute',
  pointerEvents: 'none',
  display: 'flex',
  alignItems: 'center',
  justifyContent: 'center',
}));

const StyledInputBase = styled(InputBase)(({ theme }) => ({
  color: 'inherit',
  '& .MuiInputBase-input': {
    padding: theme.spacing(1, 1, 1, 0),
    // vertical padding + font size from searchIcon
    paddingLeft: `calc(1em + ${theme.spacing(4)})`,
    transition: theme.transitions.create('width'),
    width: '100%',
    [theme.breakpoints.up('md')]: {
      width: '20ch',
    },
  },
}));

function geticon(index){
  if(index == 0){
    return(<StoreIcon/>);
  }
  else if(index == 1){
    return(<LocalGroceryStoreIcon/>);
  }
  else if(index == 2){
    return (<LocalOfferIcon/>)
  }
  else if(index == 3){
     return (<PeopleIcon/>)
  }
  else if(index == 4){
    return (<MessageIcon/>)
  }
  else{
    return(<MailIcon/>)
  }
}


export default function PrimarySearchAppBar() {
  const classes = useStyles();
  const [anchorEl, setAnchorEl] = React.useState(null);
  const [mobileMoreAnchorEl, setMobileMoreAnchorEl] = React.useState(null);
  const [open, setOpen] = React.useState(false);
  const isMenuOpen = Boolean(anchorEl);
  const isMobileMenuOpen = Boolean(mobileMoreAnchorEl);
  const [state, setState] = React.useState(false);
  const [loginState , setLoginState] = React.useState(false)
  const [search , setSearch] = React.useState("")


  React.useEffect(() => {
    //??????Login ??????
    var userId = localStorage.getItem("userId")
    
    if(userId != ""  && typeof userId != "undefined"){
      setLoginState(true);
    }
    console.log(`userId = ${userId}`)
}, [])

  
  const transPage = (page) => {
    console.log(page)
    switch (page) {
      case '????????????':
        document.location.href = "/MyStore";
        break;
      case '???????????????':
        document.location.href = "/MyBuyList";
        break;
      case '????????????':
        document.location.href = "/MyOrder";
        break;
      case '???????????????':
        document.location.href = "/FocusStore";
        break;
      case '??????????????????':
        document.location.href = "/TransHistory";
        break;
      case '????????????':
        document.location.href = "/UserInformation";
        break;
      case 'Line Bot??????':
        document.location.href = "/LineBotInformation";
        break;
      case '????????????':
        break;
    }
  }

  let barState = (loginResult) => {
    var view = ""
    var userLogin = loginResult;
    if (userLogin) {
      //???????????????????????????
      view =
        <Box>
          <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
            <IconButton size="large" aria-label="show 4 new mails" color="inherit">
              <Badge badgeContent={4} color="error">
                <MailIcon />
              </Badge>
            </IconButton>
            <IconButton
              size="large"
              aria-label="show 17 new notifications"
              color="inherit"
            >
              <Badge badgeContent={17} color="error">
                <NotificationsIcon />
              </Badge>
            </IconButton>
            <IconButton
              size="large"
              edge="end"
              aria-label="account of current user"
              aria-controls={menuId}
              aria-haspopup="true"
              onClick={handleProfileMenuOpen}
              color="inherit"
            >
              <AccountCircle />
            </IconButton>
          </Box>
          <Box sx={{ display: { xs: 'flex', md: 'none' } }}>
            <IconButton
              size="large"
              aria-label="show more"
              aria-controls={mobileMenuId}
              aria-haspopup="true"
              onClick={handleMobileMenuOpen}
              color="inherit"
            >
              <MoreIcon />
            </IconButton>
          </Box>
        </Box>
    }
    else {
      view =
        <div>
          <Button color="inherit" onClick={()=>setOpen(true)}>??????</Button>
          <Button id="Register" color="inherit" onClick={handleClick}>??????</Button>
        </div>
    }
    return view
  }

  const toggleDrawer = (open) => (event) => {
    if (event.type === 'keydown' && (event.key === 'Tab' || event.key === 'Shift')) {
      return;
    }
    setState(open);
  };

  const list = (anchor) => (
    <Box
      sx={{ width: anchor === 'top' || anchor === 'bottom' ? 'auto' : 250 }}
      role="presentation"
      onClick={toggleDrawer(anchor, false)}
      onKeyDown={toggleDrawer(anchor, false)}
    >
      <List>
        {['????????????', '???????????????', '????????????'].map((text, index) => (
          <ListItem button key={text} onClick={() => transPage(text)}>
            <ListItemIcon>
            {geticon(index)}
            </ListItemIcon>
            <ListItemText primary={text} />
          </ListItem>
        ))}
      </List>
      <Divider />
      <List>
        {['????????????', 'Line Bot??????'].map((text, index) => (
          <ListItem button key={text} onClick={() => transPage(text)}>
            <ListItemIcon>
            {geticon(index+3)}
            </ListItemIcon>
            <ListItemText primary={text} />
          </ListItem>
        ))}
      </List>
    </Box>
  );

  const handleProfileMenuOpen = (event) => {
    setAnchorEl(event.currentTarget);
  };

  const handleMobileMenuClose = () => {
    setMobileMoreAnchorEl(null);
  };

  const handleMenuClose = () => {
    setAnchorEl(null);
    handleMobileMenuClose();
  };

  const handleMobileMenuOpen = (event) => {
    setMobileMoreAnchorEl(event.currentTarget);
  };

  const handleMailClick = () => {
    console.log("Mail Click");
  }


  const handleGotoShop = () => {
    console.log("??????????????????");
    handleMenuClose();
  }

  const handleLoginForm = () => {
    console.log("??????Login Form")
  }

  const handleGotoAccountSetting = () => {
    console.log("????????????????????????");
    handleMenuClose();
  }

  const handleLogout = () => {
    localStorage.setItem("userId","");
    localStorage.setItem("storeId","");
    localStorage.setItem("userName","");
    window.location.reload()
    handleMenuClose();
  }

  const handleTextChange = event =>{
    setSearch(event.target.value)
  }

  const handleSearchEnter = event =>{
    if(event.key === 'Enter')
    axios.get(`api/Commodity/Search/${search}`, config)
    .then(response => {
      if (response.data.isSuccess) {
          //?????? comm ID
          document.location.href = `/Commodity?CommodityId=${response.data.message}&StoreId=${1}`;
      }
      else{
        alert("???????????????!")
      }
    })
  }

  const handleClick = event =>{
    console.log(event.target.id)
    switch(event.target.id){
      case 'Title':
        document.location.href = "/";
        break;
      case 'Register':
        document.location.href = "/Register";
        break;
        
    }
  }


  const menuId = 'primary-search-account-menu';
  const renderMenu = (
    <Menu
      anchorEl={anchorEl}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      id={menuId}
      keepMounted
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      open={isMenuOpen}
      onClose={handleMenuClose}
    >
      <MenuItem onClick={handleGotoShop}>????????????</MenuItem>
      <MenuItem onClick={handleGotoAccountSetting}>????????????</MenuItem>
      <MenuItem onClick={handleLogout}>??????</MenuItem>
    </Menu>
  );

  const mobileMenuId = 'primary-search-account-menu-mobile';
  const renderMobileMenu = (
    <Menu
      anchorEl={mobileMoreAnchorEl}
      anchorOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      id={mobileMenuId}
      keepMounted
      transformOrigin={{
        vertical: 'top',
        horizontal: 'right',
      }}
      open={isMobileMenuOpen}
      onClose={handleMobileMenuClose}
    >
      <MenuItem onClick={handleMailClick}>
        <IconButton size="large" aria-label="show 4 new mails" color="inherit">
          <Badge badgeContent={4} color="error">
            <MailIcon />
          </Badge>
        </IconButton>
        <p>????????????</p>
      </MenuItem>
      <MenuItem>
        <IconButton
          size="large"
          aria-label="show 17 new notifications"
          color="inherit"
        >
          <Badge badgeContent={17} color="error">
            <NotificationsIcon />
          </Badge>
        </IconButton>
        <p>????????????</p>
      </MenuItem>
      <MenuItem onClick={handleProfileMenuOpen}>
        <IconButton
          size="large"
          aria-label="account of current user"
          aria-controls="primary-search-account-menu"
          aria-haspopup="true"
          color="inherit"
        >
          <AccountCircle />
        </IconButton>
        <p>????????????</p>
      </MenuItem>
    </Menu>
  );

  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar style={{ backgroundColor: "#00AEAE" }} position="static">
        <Toolbar>
          <IconButton
            size="large"
            edge="start"
            color="inherit"
            aria-label="open drawer"
            sx={{ mr: 2 }}
          >
            <MenuIcon onClick={toggleDrawer('left', true)} />
          </IconButton>
          <Typography
            variant="h6"
            noWrap
            component="div"
            sx={{ display: { xs: 'none', sm: 'block' } }}
          >
            {/* Title Name */}
            <p id='Title' onClick={handleClick}>
              ??????????????????
            </p>

          </Typography>
          <Search>
            <SearchIconWrapper>
              <SearchIcon />
            </SearchIconWrapper>
            <StyledInputBase
              placeholder="????????????"
              inputProps={{ 'aria-label': 'search' }}
              onChange={handleTextChange}
              onKeyDown={handleSearchEnter}
            />
          </Search>
          <Box sx={{ flexGrow: 1 }} />
          {barState(loginState)}
        </Toolbar>
      </AppBar>
      {renderMenu}
      <Dialog open={open} onClose={()=>setOpen(false)} aria-labelledby="form-dialog-title">
        <DialogTitle id="form-dialog-title" style = {{"text-align":"center"}}>?????????????????????</DialogTitle>
        <Container component="main" maxWidth="xs">
          <CssBaseline />
          <div className={classes.paper}>
            <Avatar className={classes.avatar}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              ??????
          </Typography>
            <LoginForm />
          </div>
          <Box mt={5}>
          </Box>
        </Container>
        <DialogActions>
          <Button onClick={()=>setOpen(false)} color="primary">
            ??????
          </Button>
        </DialogActions>
      </Dialog>


      <Drawer
        open={state}
        onClose={toggleDrawer(false)}
      >
        {list()}
       
      </Drawer>
    </Box>
  );
}
