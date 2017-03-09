

     

      var i = 1;




function get()
{
            
    //           var quest = document.getElementById('question');
    var articleDiv = document.getElementById('Question[0].Text');

    //           var newquest = quest.cloneNode(true);
    var newArticleDiv = articleDiv.cloneNode(true);

    //var answer = document.getElementById('answer');
    //var newanswer = answer.cloneNode(true)

    //  newArticleDiv.setAttribute("id", "Question["+i+"].Text");
    newArticleDiv.setAttribute("name", "Question[" + i + "].Text");
        

     

    var elemText = document.createTextNode("Question");
    var answers = document.createTextNode("Answers");
    var elemt = document.createElement("br");
    // f.appendChild(elemt);
    //          f.appendChild(newquest);
    var br = document.createElement("br");
    var br1 = document.createElement("br");


    f.appendChild(elemText);
    f.appendChild(br);
    //  f.appendChild(newquest);
    // f.appendChild(elemt);
               
    f.appendChild(newArticleDiv);
    f.appendChild(elemt);
    f.appendChild(answers);
    f.appendChild(br1);
               
    //f.appendChild(newanswer);
             
    //  f.appendChild(elemt);
    var checkbox = document.createElement("input");
              

    var dropid = document.getElementById('Question[0].Answer[0].Text');
    //  var check = document.getElementById('Question[0].Answer[0].IsCorrect');
    var j = 0;
        
    for ( j ; j < 4; j++)
    {
                  
                   
                
        var c= checkbox.cloneNode(true);
        var nD = dropid.cloneNode(true);
        c.setAttribute("id", "Question_" + i + "__Answer_" + j + "__IsCorrect");
        c.setAttribute("name", "Question[" + i + "].Answer[" + j + "].IsCorrect");
        c.setAttribute("type", "checkbox");
        c.setAttribute("value", "true");
        // alert(dropid);
        nD.setAttribute("id", "Question[" + i + "].Answer[" + j + "].Text");
        nD.setAttribute("name", "Question[" + i + "].Answer[" + j + "].Text");
        //    ncheck.setAttribute("name", "Question[" + i + "].Answer[" + j + "].IsCorrect");
        //// nD.setAttribute("value", "");
        var elem = document.createElement("br");
        //f.appendChild(elem);
        f.appendChild(nD);
        f.appendChild(c);
        f.appendChild(elem);
        //  var ncheck = check.cloneNode(true);
        // f.appendChild(ncheck);
        // alert(j); f.appendChild(elemt);
    }
              
    //  f.appendChild(elemt);
                
             
    i++;

              

}
