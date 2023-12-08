﻿// See https://aka.ms/new-console-template for more information

using System.Linq;

Console.WriteLine("Hello, World!");

SolvePart1("bvwbjplbgvbhsrlpgdmjqwftvncz", 5);
SolvePart1("nppdvjthqldpwncqszvftbrmjlhg", 6);
SolvePart1("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10);
SolvePart1("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11);

SolvePart1("hjchjcjhjshjsssrfsrrldrddbrrzfzjzzrffvwwclwlffhwhwpwfffcbctbccchmccfmmwdmmdwwdttwffsfshswhhfchfchcphpnnflnlznlnnnvpnnhjhrrjgrglgwwrgghzhnzztlltbtwbbvmmzppdmmhchnccspccvmvwwzpwzzmddjmdmbdmmrzmmhlhhhdndllrlgrllmlhljlmmgdmggwdggffblbmmdgdwgdgwgvgcgtctjjnfnsffbqbwbnnsbbqsswggncgncntccqqfmmlqllllvrvlrlgldlggjvvqdvqvzvzpzrrvfvcfcqfcflfjfjrrwbrrpnrrvzzbddfdgfdgfddppbdbbjddtcdtccllvccjtctmccsttfcfppmvppbvpvjpvprvvmggjffbqbbqhbbcdbdrbrjrllgmggwdwzddzdczddgpgglgpgqgmgllrqlqmlqllmbmzmdzdbzddqbbzwwjfjqqlrrzgrrlzzdczddlflpfpqfqrqzzpdzdnznwwvjjnndldpdppfgppgwwnddmzzmffvgggphpbpnbblqbqccswwlcwlltvvlggqvqhvvtstmssbvvflvvhdhggqpgqgqlqggjvjpphhqnhqhvqhvqvpvvfvqfqvvbmbtmmqpqccwcbbqwbbmjbjsscsqqcccbjbvvmsmrmwrwwbhwwdpwphprptrrdssrprjrdrssqtqzqtzzcbbvrrpwrwlwbbpwwrddfcfccvttdsshqhqddsmslsffdsdrrswsmmztzhzghgqhqbbfcctmcmmdwwpbpdbbnjbnbmnmqqqftfdfnfzfqzffmmnqqgfgwwntnwwfsfmssnzzscsmmzttdwttfppnccngggmrmbmccrbcrclcslsfspfsswnwpwjwddbnbjbrjbrjbrjbrrlgljggcpgptpltlmlnnjpnptnppdcdwcwfcwcnnzddbrrnbnnsjsnjjjsqjjqhjjbrjbbghbgggsdshswsrwwqbwqqmtqmqdmdttwfwzztwztwtfwtwtfwwjpwjjljcccdbdndmmhjhzzszfssgbbwbhhhrhfrhhwttwltwtbwbrbqbpbwwjtwwdjjwhwmmddhgdhhhdrhrjjngjgnjnfjnjrjtrrhmmjzzsjzjhjmmvnmnqqzbbtnncffnhnhgnhggcssvrrwfwjwgjwwwdtwddgrrwnwffndnbdnbnsnbsnndtdvvdtdsdhshcczqzztzfzdzndnnhqqzggwrwtrtvtjvjvsjjzhzggtffdbbzwbbnwwnhnpnznfncnqqvmvbmbcbgcgfggtpggjqggnppwmwzzqpzzvvgqqjjhrrmmfsfvvbqvbqqptpztpzzhnndgngdngdngnllslhhsvssdffwllchlccjpjdpddgcgrgttmqttjlttphhdwdtdtnnrggmhhmhmgghnhbhppfmmlrmmlqqsllwswpsshbbfjjqvvlsvvblbljltjtjsszcscmsmgmddzsdddznnvvddwbwjwwmqwqlqvqffptfpttgngpgttlglpgppssthshwhzhshfshssstppbpjjtgjttlsszmzccrllwjjcdccgmccdppnlnrlltntcnttrhtrtqqfhhjfffldfdldbbqjqzzqjzzfhhtvvrrfqfsqfftrtbblmlsltsszqqcsshllvlbbpgpzgpgcppwnpwnwpnnjjmllrhllfzlzglzlnlrrcssjjjtgjgjjznjnmmjjpccqrqzrzzmfzfczcssnttddfjdfjfpjjmqjqnqtqbqzzqqzwwzwqqfccvcrvrqrlqrlqqnvjtqswzvngfcjpmnrnvnwtwnjvsmzhtwzpjbpglchwfvwhvznsvhvwwjppmqqpcpmzrznqrlvbgdfcpgdtfhwdclvzjqlhtbdvsgpjlrgbcjblnqhffbcjfwsgssfzlsbhrptfgsfsstzbwqcsrpgftblrnldhwfwpgpffftsjgclzqmjmcvwjrsbhgdblswrwnhpjtgsggmnjqgzzctjjztwhcqvhqfvddljjtqwgpmwdsmmhdttvdpqpvsqbpwmtzgfthtmfhmplmwqcbmdmrwqmmzmjmfdbqspmshlhtbmbcpcjsgdccwmbfwvftlshtrgzvbndqvqjzqgbgrnmzbwfgntfphjrvhrgzgdqclvpvwffghthlqwlghfqrwpdmgnthqwznqsjrnnpghfcfwctpvnbnftczlhmdslfvqprhgqmzmzsjvtzfsfzlcrltjhfgmwqcvnzmttfvvbsjslqfwmnhgbbjdwfgbzsjqfsgvphvmclfgtmcvlpslpqfsbzgccqslmrgdwrtlrzbbvrjrnnnncgrnsggzjrfqtmhjdvdfbwdmqrjbghrbnhpqcdzgbqwrvrcpwdlbvrdpfhnpbncjzgmftjhvwnplmnlfnlfjsjnqhtgqldzqlrlqtdjndjpsfdcdfrwtqblzpsqjvnqchdhwvswrmczhsbpfggsvzdznqjlrjjbcjnsjvqtrtttmmcgdwbqcthqvzffjmdbvjmjvcrmnpjgtjshbnlqpdfdnbcfmbrzsvqftrnfzmjdhpprpnwqbngmbbwjvmdzbwvttncdtgqnwwchmbbdtrwlflmqnbthnczfpmtpfpqpbwbcpsplgfpjfptdpvzjnbgzrfdwpdrztqtsrzmbfqhgwnfzcsbdsjsmbdghjcjlvbpjpplgqqnbqpqgsqqbmpgmlghrlbcfzjhlqfgdpfljspwqjbsjqzwwhrcpfrhwpvgrjpqjzfzphcrwbfwsjdsjtlwctsfhrmbnvsrfwwvgqtjtjvvqzjznlrsblgthjfrphsfmbtpmbthwdhrqbdmbzplbvpbcwvhgrsjccsnhbrqdbljzpdttbffqrbmgmzsmhdhsmnnmjqtwdjpmhlpwtwhvbfnjzfcwfzfplsbqgcvwgjcbwzbzmqdchwrggjwgjbttsttsztrftttqpslwvtcrjmdtwdhlwnhpjstqnqtvrmlmtcgjljzrthgpmvdjzlwfntqmbpdpgmmvvwqmdqqwrnlsrmhrpdtmhjrngwdfgddlrmdnfdnscjhdfjzwljjrsclnfdmhpbsvwtsmrdsvmpbjjpmmgtfclcccmcnzcslsvdwncrgtpvgbcgwdmcqlthgmrqmpnprlsqgzzpzzmhgflfgpjwgjjdpvggmhcstrwscqggqgrrjwtqzdfbnwgtvslvghfnzphbznqslcwwcsplgwjnltrttqzcvjhfdrwgjpclzmqfgvhzhrcdsgchhfqptqjwffmrsjrplzzlhnptwlmrvtstsrgnfdnrbtdbzjdcbthhtnjdprrpgtgfjsqnpgslcqgmdfpsrdfvhbqvvpthmmshpdnrrwlfcmqfbrsvqdqhffgbdhwjgcjsclcqpwnrfzfdqcvnmqnjmjnvhqmznmbnbnnjfrtlvbpdgglqpgcmqqcnpzfvvsnchpbjprpnwbdqvqzgjvgtnsrvswfmwhzllmlgpsglssnhcvbjtfghhrznpzntwwtnshmhhddnntdljhhhpmnchssqthbzpqmtmjbcfvmgnmwhpzrbwzzvzmnfdcsbvzphlglbhjpfmrtgfblhtszqvbbmtglwdhgjdvvgtpscgvwzjppfnlndnmtrnnnlfbgmrpqlvhvbgzmwghnsmdmdrftqpqncsbcmqhhhljzlwcrlsdbhrlddwlhcghvttjfsmfdzcllswjgsmcmghbflbdgpwfqplqnrvzfnctdsnmldhtbtpfrsztjdsgmnbrdjwbrgqlhdrlrnmlpwltgpwhwztbwpcqtwbqdmsfdfczftncvsggshhcqbjgcwjljcqdpczrnzbjhrhwcgrbbqzmmfjpqwrwppmnvcsfwprjqvtnzqzwtwlvvqssfjzbrvjjrmphtbjbrzttmvvhdfsnqdmpfbtprbqgzdgtjtpvbqqsgppsrnvsfnmgvbbsjcpttffthpvfjpnzmsjmpdzbldggtjrjqpshtmgpfgtcstdrgjhzjr");

void SolvePart1(string input, int? expectedAnswer = null)
{
    var chars = input.ToCharArray();
    int answer = -1;
    for (int i = 3; i < chars.Length; i++)
    {
        var lastFour = new[] { chars[i - 3], chars[i - 2], chars[i - 1], chars[i] };
        var groups = lastFour.GroupBy(x => x);
        if (groups.Count() == 4)
        {
            answer = i + 1;
            break;
        }
    }

    string expectedAnswerString = expectedAnswer != null ? $" expected answer: {expectedAnswer.Value}" : "";
    Console.WriteLine($"From input: {input.Substring(0, 5)} - answer: {answer} {expectedAnswerString}");
}