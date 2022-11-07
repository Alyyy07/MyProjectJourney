const express = require("express");
const expressLayouts = require("express-ejs-layouts");

const {body,check,validationResult} = require("express-validator");
const methodOverride = require("method-override");

const session = require("express-session");
const flash = require("connect-flash");
const cookieParser = require("cookie-parser");

require("./utils/db");
const Contact = require("./model/contact");

const app = express();
const port = 3000;

// konfigurasi view engine
app.set("view engine", "ejs");
app.use(expressLayouts);
app.use(express.static("public"));

app.use(methodOverride("_method"));

//konfigurasi flash
app.use(cookieParser("secret"));
app.use(
  session({
    cookie: { maxAge: 60000 },
    secret: "secret",
    resave: true,
    saveUninitialized: true,
  })
);
app.use(flash());

app
  .get("/", (req, res) => {
    res.render("index", {
      layout: "partials/main-layout",
      nama: "Aly Akbar",
      title: "Halaman Home"
    });
  })
  .get("/about", (req, res) =>
    res.render("about", {
      title: "Halaman About",
      layout: "partials/main-layout",
    })
  )
  .get("/contact", async (req, res) => {
    // jika tanpa menggunakan async await
    // Contact.find().then(contact => {
    //     res.send(contact);

    const contacts = await Contact.find();
    res.render("contact", {
      title: "Halaman Contact",
      layout: "partials/main-layout",
      contacts,
      msg: req.flash("msg"),
    });
  })

  // halaman form tambah contact
  .get('/contact/add',(req,res)=>{
    res.render('add-contact',{
      title:'Form Tambah Data Kontak',
      layout:'partials/main-layout',
    });
  })

  // proses tambah data contact
  .post(
    "/contact",
    [
     body("nama").custom(async (value) => {
        const duplikat = await Contact.findOne({ nama: value });
        if (duplikat) throw new Error("Nama sudah ada");
        return true;
      }),
      check("email", "Email tidak Valid").isEmail(),
      check("nohp", "no HP tidak valid").isMobilePhone("id-ID"),
    ],
    (req, res) => {
      const errors = validationResult(req);
      if (!errors.isEmpty()) {
        res.render("add-contact", {
          title: "Form Tambah Data Contact",
          layout: "partials/main-layout",
          errors: errors.array(),
        });
      } else {
        Contact.insertMany(req.body,(err,result)=>{
          //mengirimkan flash message
          req.flash("msg", "Data berhasil ditambahkan");
          res.redirect("/contact");
        })
      }
    }
  )

  // proses delete contact
.delete("/contact",(req, res) => {
  Contact.deleteOne({nama:req.body.nama}).then(result =>{
    req.flash("msg", "Data berhasil dihapus");
    res.redirect("/contact");
  })
})

  // halaman edit data contact
  .get("/contact/edit/:nama",async (req,res)=>{
    const contact =await Contact.findOne({nama:req.params.nama});
    res.render("edit-contact",{
      title:"Form Edit Data Contact",
      layout:"partials/main-layout",
      contact,
    });
    
  })

  .put(
    "/contact",
    [
      body("nama").custom(async (value,{req}) => {
        const duplikat = await Contact.findOne({nama:value});
        if (value !== req.body.oldName && duplikat) throw new Error("Nama sudah ada");
        return true;
      }),
      check("email", "Email tidak Valid").isEmail(),
      check("nohp", "no HP tidak valid").isMobilePhone("id-ID"),
    ],
    (req, res) => {
      const errors = validationResult(req);
      if (!errors.isEmpty()) {
        res.render("edit-contact", {
          title: "Form Ubah Data Contact",
          layout: "partials/main-layout",
          errors: errors.array(),
          contact: req.body
        });
      } else {
        Contact.updateOne({_id:req.body._id},
          {
            $set: {
              nama: req.body.nama,
              email: req.body.email,
              nohp: req.body.nohp,
            }
          }
        ).then(result =>{
        //mengirimkan flash message
        req.flash("msg", "Data berhasil diubah");
        res.redirect("/contact");
      })
      }
    }
  )

  // halaman form detail contact
  .get("/contact/:nama", async (req, res) => {
    const contact = await Contact.findOne({ nama: req.params.nama });
    res.render("detail", {
      title: "Halaman Detail Contact",
      layout: "partials/main-layout",
      contact,
    });
  })
  .use("/", (req, res) => {
    res.status(404);
    res.send("<h1> 404 Not Found </h1>");
  })
  .listen(port, () => {
    console.log(`listening at http://localhost:${port}`);
  });
