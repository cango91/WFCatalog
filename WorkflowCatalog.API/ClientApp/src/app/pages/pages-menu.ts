import { NbMenuItem } from '@nebular/theme';


export const MENU_ITEMS: NbMenuItem[] = [

  {
    title: 'Dashboard',
    icon: 'home-outline',
    link: '/pages/dashboard',
    home: true,
  },
  {
    title: 'Function Catalog',
    children: [
      {
        title: 'Team',
        link: '/pages/dashboard',
      },
      {
        title: 'Cerebral Plus',
        link: '/pages/dashboard',
      },
      {
        title: 'Acıbadem Online',
        link: '/pages/dashboard'
      },
      {
        title: 'i-Ticket',
        link: '/pages/dashboard',
      }
    ]
  },
  {
    title: 'Workflow Catalog',
    expanded: true,
    children:[]
  }
  /*
  {
    title: 'Dashboard',
    icon: 'home-outline',
    link: '/pages/dashboard',
    home: true,
  },
  {
    title: 'Function Catalog',
    group: true,
  },
  {
    title: 'Team',
    link: '/pages/dashboard',
  },
  {
    title: 'Cerebral Plus',
    link: '/pages/dashboard',
  },
  {
    title: 'Acıbadem Online',
    link: '/pages/dashboard'
  },
  {
    title: 'i-Ticket',
    link: '/pages/dashboard',
  },
  {
    title: 'Workflow Catalog',
    group: true,
  },

  */
];
