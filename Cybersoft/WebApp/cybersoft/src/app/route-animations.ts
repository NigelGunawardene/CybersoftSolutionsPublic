import {
  trigger,
  transition,
  style,
  query,
  group,
  animateChild,
  animate,
  keyframes,
  state,
} from '@angular/animations';

export const neucolor : string = 'rgb(255, 255, 255)'; // 'rgb(210, 238, 237)'

export const fader =
  trigger('fadeAnimation', [
    transition('* <=> *', [
      // Set a default  style for enter and leave
      query(':enter, :leave', [
        style({
          position: 'absolute',
          left: 0,
          width: '100%',
          opacity: 0,
          transform: 'scale(0) translateY(100%)',
        }),
      ]),
      // Animate the new page in
      query(':enter', [
        animate('600ms ease', style({ opacity: 1, transform: 'scale(1) translateY(0)' })),
      ])
    ]),
  ]);

export const slider =
  trigger('slideAnimation', [
    transition('* => isLeft', slideTo('left') ),
    transition('* => isRight', slideTo('right') ),
    transition('isRight => *', slideTo('left') ),
    transition('isLeft => *', slideTo('right') )
  ]);

function slideTo(direction) {
  const optional = { optional: true };
  return [
    query(':enter, :leave', [
      style({
        position: 'absolute',
        top: 0,
        [direction]: 0,
        width: '100%',
      })
    ], optional),
    query(':enter', [
      style({ [direction]: '-100%'})
    ]),
    group([
      query(':leave', [
        animate('600ms ease', style({  opacity: 0, [direction]: '100%'}))
      ], optional),
      query(':enter', [
        animate('600ms ease', style({ [direction]: '0%'}))
      ])
    ]),
    // Normalize the page style... Might not be necessary

    // Required only if you have child animations on the page
    // query(':leave', animateChild()),
    // query(':enter', animateChild()),
  ];
}


export const transformer =
  trigger('routeAnimations', [
    transition('* => isLeft', transformTo({ x: -100, y: -100, rotate: -720 }) ),
    transition('* => isRight', transformTo({ x: 100, y: -100, rotate: 90 }) ),
    transition('isRight => *', transformTo({ x: -100, y: -100, rotate: 360 }) ),
    transition('isLeft => *', transformTo({ x: 100, y: -100, rotate: -360 }) )
  ]);


function transformTo({x = 100, y = 0, rotate = 0}) {
  const optional = { optional: true };
  return [
    query(':enter, :leave', [
      style({
        position: 'absolute',
        top: 0,
        left: 0,
        width: '100%'
      })
    ], optional),
    query(':enter', [
      style({ transform: `translate(${x}%, ${y}%) rotate(${rotate}deg)`})
    ]),
    group([
      query(':leave', [
        animate('600ms ease-out', style({ transform: `translate(${x}%, ${y}%) rotate(${rotate}deg)`}))
      ], optional),
      query(':enter', [
        animate('600ms ease-out', style({ transform: `translate(0, 0) rotate(0)`}))
      ])
    ]),
  ];
}



export const stepper =
  trigger('routeAnimations', [
    transition('* <=> *', [
      query(':enter, :leave', [
        style({
          position: 'absolute',
          left: 0,
          width: '100%',
        }),
      ]),
      group([
        query(':enter', [
          animate('2000ms ease', keyframes([
            style({ transform: 'scale(0) translateX(100%)', offset: 0 }),
            style({ transform: 'scale(0.5) translateX(25%)', offset: 0.3 }),
            style({ transform: 'scale(1) translateX(0%)', offset: 1 }),
          ])),
        ]),
        query(':leave', [
          animate('2000ms ease', keyframes([
            style({ transform: 'scale(1)', offset: 0 }),
            style({ transform: 'scale(0.5) translateX(-25%) rotate(0)', offset: 0.35 }),
            style({ opacity: 0, transform: 'translateX(-50%) rotate(-180deg) scale(6)', offset: 1 }),
          ])),
        ])
      ]),
    ])

  ]);

export const fadeInAnimation =
  // trigger name for attaching this animation to an element using the [@triggerName] syntax
  trigger('fadeInAnimation', [

    // route 'enter' transition
    transition(':enter', [

      // css styles at start of transition
      style({ opacity: 0 }),

      // animation and styles at end of transition
      animate('1s', style({ opacity: 1 }))
    ]),
  ]);


export const longFadeInAnimation =
  // trigger name for attaching this animation to an element using the [@triggerName] syntax
  trigger('longFadeInAnimation', [

    // route 'enter' transition
    transition(':enter', [

      // css styles at start of transition
      style({ opacity: 0 }),

      // animation and styles at end of transition
      animate('3s', style({ opacity: 1 }))
    ]),
  ]);


export const slideInOutAnimation =
  // trigger name for attaching this animation to an element using the [@triggerName] syntax
  trigger('slideInOutAnimation', [

    // end state styles for route container (host)
    state('*', style({
      // the view covers the whole screen with a semi tranparent background
      position: 'fixed',
      top: 0,
      left: 0,
      right: 0,
      bottom: 0,
      backgroundColor: 'rgba(0, 0, 0, 0.8)'
    })),

    // route 'enter' transition
    transition(':enter', [

      // styles at start of transition
      style({
        // start with the content positioned off the right of the screen,
        // -400% is required instead of -100% because the negative position adds to the width of the element
        right: '-400%',

        // start with background opacity set to 0 (invisible)
        backgroundColor: 'rgba(0, 0, 0, 0)'
      }),

      // animation and styles at end of transition
      animate('.5s ease-in-out', style({
        // transition the right position to 0 which slides the content into view
        right: 0,

        // transition the background opacity to 0.8 to fade it in
        backgroundColor: 'rgba(0, 0, 0, 0.8)'
      }))
    ]),

    // route 'leave' transition
    transition(':leave', [
      // animation and styles at end of transition
      animate('.5s ease-in-out', style({
        // transition the right position to -400% which slides the content out of view
        right: '-400%',

        // transition the background opacity to 0 to fade it out
        backgroundColor: 'rgba(0, 0, 0, 0)'
      }))
    ])
  ]);



export const addToCartAnimation =
  trigger('addToCart', [
    // ...
    state('adding', style({
      height: '100%',
      opacity: 1,
      background: neucolor
    })),
    state('added', style({
      height: '100%',
      opacity: 1,
      background: neucolor
    })),
    transition('adding => added', [
      animate('220ms', style(
        {background: 'rgba(69, 217, 234, 0.8)'}
      )),
      animate('220ms', style(
        {background: neucolor}
      ))
    ]),
    transition('added => adding', [
      animate('220ms', style(
        {background: 'rgba(69, 217, 234, 0.8)'}
      )),
      animate('220ms', style(
        {background: neucolor}
      ))
    ]),
  ]);



export const logInFadeIn =
  trigger('logInFadeIn', [
   transition('void => *', [
     style({
       opacity: 0
     }),
     animate(800, style({opacity: 1}))
   ])
  ]);

export const fadeOut =
  trigger('fadeOut', [
    transition('void => *', [
      style({
        opacity: 0
      }),
      animate(300, style({opacity: 1}))
    ]),
    transition('* => void', [
      style({
        opacity: 1
      }),
      animate(400, style({opacity: 0}))
    ])
  ]);



export const flyInOut =
  trigger('flyInOut', [
    state('in', style({
      width: '*',
      transform: 'translateX(0)', opacity: 1
    })),
    transition(':enter', [
      style({ width: 10, transform: 'translateX(50px)', opacity: 0 }),
      group([
        animate('0.3s 0.1s ease', style({
          transform: 'translateX(0)',
          width: '*'
        })),
        animate('0.3s ease', style({
          opacity: 1
        }))
      ])
    ]),
    transition(':leave', [
      group([
        animate('0.3s ease', style({
          transform: 'translateX(50px)',
          width: 10
        })),
        animate('0.3s 0.2s ease', style({
          opacity: 0
        }))
      ])
    ])
  ])

// to read up on this use the following link - https://fluin.io/blog/hierarchical-route-animations
export const cubicBezierInAndOut =
  trigger('routeAnimation', [
    transition('1 => 2, 2 => 3', [
      style({ height: '!' }),
      query(':enter', style({ transform: 'translateX(100%)' })),
      query(':enter, :leave', style({ position: 'absolute', top: 0, left: 0, right: 0 })),
      // animate the leave page away
      group([
        query(':leave', [
          animate('0.3s cubic-bezier(.35,0,.25,1)', style({ transform: 'translateX(-100%)' })),
        ]),
        // and now reveal the enter
        query(':enter', animate('0.3s cubic-bezier(.35,0,.25,1)', style({ transform: 'translateX(0)' }))),
      ]),
    ]),
    transition('3 => 2, 2 => 1', [
      style({ height: '!' }),
      query(':enter', style({ transform: 'translateX(-100%)' })),
      query(':enter, :leave', style({ position: 'absolute', top: 0, left: 0, right: 0 })),
      // animate the leave page away
      group([
        query(':leave', [
          animate('0.3s cubic-bezier(.35,0,.25,1)', style({ transform: 'translateX(100%)' })),
        ]),
        // and now reveal the enter
        query(':enter', animate('0.3s cubic-bezier(.35,0,.25,1)', style({ transform: 'translateX(0)' }))),
      ]),
    ]),
  ])
